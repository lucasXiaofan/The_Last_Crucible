using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_RangeAttack1_1 : DP_State
    {
        //public DP_BossChaseState bossChaseState;


        public override DP_State Tick(DP_EnemyManger enemyManger,
                                        DP_EnemyStats enemyStats,
                                        DP_EnemyAnimator enemyAnimator,
                                        DP_EnemyLocomotion enemyLocomotion)
        {
            //Check if attack still in play?
            return this;
            //Check if attack is done
            //return bossChaseState

            //Otherwise, handle attack
            //Handle rotation
            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);

            //Play animation
            //Instantiate projectile
        }

        private void ProjectileAttack()
        {

        }
    }
}
