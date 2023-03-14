using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class PlayerAttacker : MonoBehaviour
    {
        // Start is called before the first frame update
        DP_animationHandler animationHandler;
        DP_inputHandler inputHandler;
        DP_PlayerManager playerManager;
        DP_WeaponSlotManager weaponSlotManager;
        DP_playerLomotion playerLomotion;
        DP_CameraControl cameraControl;
        public string lastAttack;
        public LayerMask backStacbLayer;
        public LayerMask executeLayer;
        void Start()
        {
            playerManager = GetComponent<DP_PlayerManager>();
            cameraControl = FindObjectOfType<DP_CameraControl>();
            playerLomotion = GetComponent<DP_playerLomotion>();
            weaponSlotManager = GetComponentInChildren<DP_WeaponSlotManager>();
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
        }
        public void HandleCombo(DP_WeaponItem weaponItem)
        {
            if (inputHandler.comboFlag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weaponItem.Oh_Light_Attack_1)
                {
                    
                    animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_2, true, false);
                }
                if (lastAttack == weaponItem.Oh_Heavy_Attack_1)
                {
                    animationHandler.ApplyTargetAnimation(weaponItem.Oh_Heavy_Attack_2, true, false);
                }
            }
        }
        public bool CanExecute()
        {
            RaycastHit hit;
            return (Physics.Raycast(playerManager.CriticalStabPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, executeLayer));
        }
        public bool CanBackStab()
        {
            RaycastHit hit;
            return (Physics.Raycast(playerManager.CriticalStabPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStacbLayer));
        }
        public void HandleExecution(bool isExecute)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerManager.CriticalStabPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f,
                                (isExecute ? executeLayer : backStacbLayer)))
            {
                DP_EnemyManger enemyManger = hit.transform.gameObject.GetComponentInParent<DP_EnemyManger>();
                if (enemyManger != null)
                {
                    playerManager.transform.position = Vector3.Lerp(playerManager.transform.position,
                            (isExecute ? enemyManger.ExecutePoint.position : enemyManger.BackStabPoint.position),
                                                                100f * Time.deltaTime);
                    //do rotation           
                    Vector3 RotationDirection = playerManager.transform.root.eulerAngles;
                    RotationDirection = hit.transform.position - playerManager.transform.position;
                    RotationDirection.y = 0;
                    RotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(RotationDirection);
                    playerManager.transform.rotation = Quaternion.Slerp(playerManager.transform.rotation, tr, Time.deltaTime * 500);
                    animationHandler.ApplyTargetAnimation((isExecute ? "Execute" : "BackStab"), true, false);
                    enemyManger.GetComponentInChildren<DP_EnemyAnimator>().ApplyTargetAnimation((isExecute ? "Executed" : "BackStabbed"), true, false);
                    enemyManger.GetComponent<DP_EnemyStats>().TakeDamage(500, false);
                }
            }

        }
        public void HandleAirAttack(DP_WeaponItem weaponItem)
        {
            weaponSlotManager.attackingWeapon = weaponItem;
            HandleRotationWhileAttack();
            animationHandler.ApplyTargetAnimation("InAirAttack", true, false);
        }
        public void HandleLightAttack(DP_WeaponItem weaponItem)
        {
            weaponSlotManager.attackingWeapon = weaponItem;
            HandleRotationWhileAttack();
            
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_1, true, false);
            lastAttack = weaponItem.Oh_Light_Attack_1;

        }
        public void HandleRotationWhileAttack()
        {
            Vector3 moveDir = playerLomotion.cameraPos.forward * inputHandler.vertical;
            moveDir += playerLomotion.cameraPos.right * inputHandler.horizontal;
            if (inputHandler.lockOnFlag)
            {
                Vector3 dir = cameraControl.currentLockOnTransform.LockOnTransform.position - cameraControl.cameraTransform.position;
                dir.Normalize();
                dir.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                //Quaternion tr = Quaternion.Slerp(playerLomotion.playerTransform.rotation, targetRotation, 15f * Time.deltaTime);
                playerLomotion.playerTransform.rotation = targetRotation;
            }
            else
            {
                if (inputHandler.moveAmount > 0)
                {

                    moveDir.y = 0;
                    playerLomotion.playerTransform.rotation = Quaternion.LookRotation(moveDir);
                }
            }

        }
        public void HandleHeavyAttack(DP_WeaponItem weaponItem)
        {
            weaponSlotManager.attackingWeapon = weaponItem;
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Heavy_Attack_1, true, false);
            lastAttack = weaponItem.Oh_Heavy_Attack_1;
            HandleRotationWhileAttack();
        }


    }
}
