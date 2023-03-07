using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class Boss_IdleState : DP_State
    {
        public DP_State CombatStanceState;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else
            {
                return CombatStanceState;
            }
            return this;
        }
    }
}
