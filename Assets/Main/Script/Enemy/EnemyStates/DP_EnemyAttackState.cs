using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyAttackState : DP_State
    {
        public DP_EnemyPursueState pursueState;
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0);
                return pursueState;
            }
            enemyManger.AttackTarget();
            return this;
        }
    }
}
