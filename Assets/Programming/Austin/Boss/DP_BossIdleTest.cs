using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Modified idle to test melee attacks. Will not chase.
namespace DP
{
    public class DP_BossIdleTest : DP_State
    {
        public DP_BossMeleeAttack attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            if (enemyLocomotion.currentTarget != null && enemyLocomotion.distanceFromtarget < 5)
            {
                return attack;
            }

            return this;
        }

    }
}
