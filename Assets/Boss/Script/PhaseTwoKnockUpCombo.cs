using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class PhaseTwoKnockUpCombo: DP_State
    {
        
        public DP_State combatStanceState;
        public int coolDown = 3;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            enemyLocomotion.navMeshAgent.enabled = false;
             #region Rotation
            Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            direction.y = 0;
            direction.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
            #endregion
            if (!(enemyLocomotion.currentTarget == null) && !(enemyManger.isPreformingAction) && !enemyManger.Recovering)
            {
               
                enemyAnimator.ApplyTargetAnimation("MA13_C2", true, false);
                enemyManger.currentRecoveryTime = coolDown;
                
            }
            else if (!(enemyManger.isPreformingAction))
            {
                return combatStanceState;
            }
            Vector3 jumpVector = enemyLocomotion.transform.position;
            jumpVector.y += enemyAnimator.anim.deltaPosition.y;
            enemyLocomotion.transform.position = jumpVector;
            return this;
            
        }
    }

}
