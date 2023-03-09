using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class Boss_Melee11Combo : DP_State
    {
        public DP_State ComboState;

        //after boss attacked player always return to this state to select next attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            
            if (!(enemyManger.isPreformingAction) && !(enemyManger.Recovering))
            {
                enemyAnimator.finishCombo = false;
                enemyLocomotion.navMeshAgent.enabled =false;
                enemyAnimator.ApplyTargetAnimation("MA11_1", true, false);
            }
            else if (enemyAnimator.finishCombo)
            {
                return ComboState;
            }
            else
            {
                #region Rotation
                Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
                direction.y = 0;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed*10 / Time.deltaTime);
                #endregion
            }
            enemyLocomotion.transform.position += enemyAnimator.anim.deltaPosition;
            return this;
            
        }
    }
}