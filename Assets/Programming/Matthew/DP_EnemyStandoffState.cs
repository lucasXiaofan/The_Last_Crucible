using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStandoffState : DP_State
    {
        //public DP_EnemyChaseState enemyChaseState;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            //USE Common_StrafeWalkL_Root and Common_StrafeWalkR_Root ANIM
            

            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }

            //If location == null, determine new location

            //Handle movement

            //Check if enemy has reached desired position
            //If so return Chase state

            //Otherwise, return this state
            return this;
        }
    }
}