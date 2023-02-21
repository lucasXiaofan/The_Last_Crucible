using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStandoffState : DP_State
    {
        public DP_EnemyPursueState enemyPursueState;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            //USE Common_StrafeWalkL_Root and Common_StrafeWalkR_Root ANIM
            return this;
        }
    }
}