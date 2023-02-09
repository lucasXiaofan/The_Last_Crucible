using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyPursueState : DP_State
    {
        public DP_EnemyAttackState enemyAttackState;
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                return enemyAttackState;
            }
            enemyLocomotion.HandleMovement();
            return this;
        }
    }

}