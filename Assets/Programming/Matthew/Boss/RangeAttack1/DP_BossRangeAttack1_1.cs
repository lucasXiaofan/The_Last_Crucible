using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_BossRangeAttack1_1 : DP_State
    {
        //public DP_BossChaseState bossChaseState;
        public DP_State tempState;

        public DP_EnemyAttackActions attackSO;
        public Transform attackOrigin;
        public GameObject projectilePrefab;
        private Quaternion targetRotation;

        private GameObject projectileObj;

        private Vector3 currentMoveLoc;
        public float navMeshNewSpeed = 1f;
        public float navMeshOldSpeed;
        public float distanceMult = 0f;
        public float coolDown = 5f;

        

        //Need to time out animation with projectile spawn
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

        //0 = back, 1 = left, 2 = right
        public void moveBoss(DP_EnemyLocomotion enemyLocomotion, DP_EnemyManger enemyManger, int moveDir)
        {
            //Determine new location
            if (moveDir == 0)
            {
                currentMoveLoc = enemyManger.transform.position - (enemyManger.transform.forward * distanceMult);
            }
            else if (moveDir == 1)
            {
                currentMoveLoc = enemyManger.transform.position - (enemyManger.transform.right * distanceMult);
            }
            else if (moveDir == 2)
            {
                currentMoveLoc = enemyManger.transform.position + (enemyManger.transform.right * distanceMult);
            }

            enemyLocomotion.navMeshAgent.SetDestination(currentMoveLoc);
            //enemyLocomotion.HandleRotationTowardsTarget();
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
