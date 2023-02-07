using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyIdleState : DP_State
    {
        public DP_EnemyPursueState enemyPursueState;
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else if (enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
            {
                return enemyPursueState;
            }
            return this;
        }
    }

}

