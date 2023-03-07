using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class PhaseTwoMeleeKnockUp : DP_State
    {
        public DP_State ComboState;
        public int coolDown = 3;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (!(enemyLocomotion.currentTarget == null) && !(enemyManger.isPreformingAction) && !(enemyManger.Recovering))
            {
                #region Rotation
                Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
                direction.y = 0;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed*10 / Time.deltaTime);
                #endregion
                enemyAnimator.ApplyTargetAnimation("MA13_C1", true, false);
                // enemyManger.currentRecoveryTime = coolDown;
                
            }
            else
            {
                return ComboState;
            }
            return this;
            
        }
    }

}

