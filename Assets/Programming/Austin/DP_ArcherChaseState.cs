using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ArcherChaseState : DP_State
    {
        public DP_ArcherIdleTest idleState;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null) return idleState;
            // Too Close
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance - 10)
            {
                return idleState;
            }
            // Within Firing Range
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                return idleState; // Replace with aim.
            }
            enemyLocomotion.HandleMovement();
            return this;
        }

    }
}
