using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questions to ask
// Should the boss be turning during these attacks?
// Any special considerations for change of state? (Should I send this back to chase or stay in state until combo is finished?)
// Note: No edits to animation conditionals made yet. So, no damage or special effects done (Sorry I don't want to overwrite stuff that ain't mine!)

namespace DP
{
    public class DP_BossMeleeAttack : DP_State
    {
        public DP_BossIdleTest chase; // Replace with chase later.
        public DP_EnemyAttackActions attack;
        bool exitFlag = false;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (!enemyManger.isPreformingAction)
            {
                if (exitFlag)
                {
                    enemyAnimator.anim.SetFloat("Vertical", 0);
                    exitFlag = false;
                    return chase;
                }
                enemyManger.currentAttack = attack;
                enemyManger.AttackTarget();
                enemyLocomotion.HandleRotationTowardsTarget();
            }
            else {
                exitFlag = true;
            }
            
            return this;
        }

    }
}
