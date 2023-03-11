using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    //SWITCH TO ANIMATION EVENTS

    public class DP_BossRangeAttack1_2 : DP_State
    {
        //public DP_BossChaseState bossChaseState;
        public DP_State tempState;

        public DP_EnemyAttackActions attackSO;
        public Transform attackOrigin;
        public GameObject projectilePrefab;
        private Quaternion targetRotation;

        private GameObject projectileObj;

        
        public float jumpHeight;
        private bool CR_running = false;
        public float coolDown = 5f;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                        DP_EnemyStats enemyStats,
                                        DP_EnemyAnimator enemyAnimator,
                                        DP_EnemyLocomotion enemyLocomotion)
        {

            if (enemyLocomotion.currentTarget == null)
            {
                
                enemyLocomotion.HandleDetection();
            }

            enemyManger.currentAttack = attackSO;
            enemyManger.AttackTarget();
            enemyManger.currentRecoveryTime = coolDown;
            return tempState;
        }

        public void startJump(DP_EnemyAnimator enemyAnimator, DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion)
        {
            StartCoroutine(jumpMotion(enemyManger, 0.83f));
        }

        public void startLand(DP_EnemyAnimator enemyAnimator, DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion)
        {
            StartCoroutine(landMotion(enemyManger, 0.83f));
        }

        //Jump function
        IEnumerator jumpMotion(DP_EnemyManger enemyManger, float time)
        {
            Vector3 startPosition = enemyManger.transform.position;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                enemyManger.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, jumpHeight, startPosition.z), Mathf.Pow((elapsedTime / time), 2.4f));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        //Land function
        IEnumerator landMotion(DP_EnemyManger enemyManger, float time)
        {
            Vector3 startPosition = enemyManger.transform.position;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                enemyManger.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, 0f, startPosition.z), Mathf.Pow((elapsedTime / time), 2.4f));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemyManger.transform.rotation = new Quaternion(0f, enemyManger.transform.rotation.y, 0f, enemyManger.transform.rotation.w);
            enemyManger.transform.position = new Vector3(enemyManger.transform.position.x, 0f, enemyManger.transform.position.z);
        }

        public void ProjectileAttack(Quaternion targetRotation)
        {
            projectileObj = Instantiate(projectilePrefab, attackOrigin.position, targetRotation);
        }

        public Quaternion HandleRotation(DP_EnemyLocomotion enemyLocomotion, DP_EnemyManger enemyManger)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }

            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);
            return targetDir;
        }
    }
}
