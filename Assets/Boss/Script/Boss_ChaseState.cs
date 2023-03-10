using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DP
{
    public class Boss_ChaseState : DP_State
    {
    
        public DP_State[] Phase2Attacks;
        
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                int attack = Random.Range(0,Phase2Attacks.Length);
                return Phase2Attacks[attack];
            }
            enemyLocomotion.HandleMovement();
            return this;
        }
    }
}
