using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_BossRangeAttack1_1 : DP_State
    {
        //public DP_BossChaseState bossChaseState;
        public DP_EnemyIdleState tempState;


        public DP_EnemyAttackActions attackSO;
        public Transform attackOrigin;
        public GameObject projectilePrefab;
        GameObject projectileObj;

        //Need to time out animation with projectile spawn
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                        DP_EnemyStats enemyStats,
                                        DP_EnemyAnimator enemyAnimator,
                                        DP_EnemyLocomotion enemyLocomotion)
        {
            var targetRotation = HandleRotation(enemyLocomotion, enemyManger);

            if (enemyManger.isPreformingAction)
            {
                return this;
            }
            else
            {
                HandleAttackTimer(enemyManger, enemyAnimator);
                ProjectileAttack(targetRotation);

                return tempState;
                //return bossChaseState;
            }
        }

        private void ProjectileAttack(Quaternion targetRotation)
        {
            //Spawn Projectile
            //VFX Pack already has hit detection?
            projectileObj = Instantiate(projectilePrefab, attackOrigin.position, targetRotation);
        }

        private Quaternion HandleRotation(DP_EnemyLocomotion enemyLocomotion, DP_EnemyManger enemyManger)
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

        private void HandleAttackTimer(DP_EnemyManger enemyManager, DP_EnemyAnimator enemyAnimator)
        {
            enemyManager.currentAttack = attackSO;
            enemyManager.AttackTarget();
        }
    }
}
