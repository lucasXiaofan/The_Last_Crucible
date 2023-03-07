using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DP
{
    public class Boss_ChaseState : DP_State
    {
        public DP_State Attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                return Attack;
            }
            enemyLocomotion.HandleMovement();
            return this;
        }
    }
}
