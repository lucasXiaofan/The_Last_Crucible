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
        DP_WeaponSlotManager weaponSlotManager;
        DP_playerLomotion playerLomotion;
        DP_CameraControl cameraControl;
        public string lastAttack;
        void Start()
        {
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

        public void HandleLightAttack(DP_WeaponItem weaponItem)
        {


            weaponSlotManager.attackingWeapon = weaponItem;
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_1, true, false);
            lastAttack = weaponItem.Oh_Light_Attack_1;
            HandleRotationWhileAttack();
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
