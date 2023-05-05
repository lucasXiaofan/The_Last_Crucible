using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyIdleState : DP_State
    {
        public DP_EnemyPursueState enemyPursueState;
        public DP_EnemyAttackState enemyAttackState;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else if (enemyLocomotion.currentTarget != null && enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
            {
                enemyStats.ShowUI();
                enemyLocomotion.ShareInformation();
                if (enemyManger.Boss || enemyManger.KeyHolder)
                {
                    enemyManger.soundManager.PlayBattleM(true);
                }
                return enemyPursueState;
            }
            else if (enemyLocomotion.currentTarget != null && enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                return enemyAttackState;
            }
            return this;
        }
    }

}

