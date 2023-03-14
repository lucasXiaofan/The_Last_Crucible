using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class Boss_Melee12Combo : DP_State
    {
        public int coolDown = 3;

        public DP_State ComboState;

        //after boss attacked player always return to this state to select next attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            Vector3 ground = Vector3.zero;

            if (!(enemyManger.isPreformingAction) && !(enemyManger.Recovering))
            {
                float groundY = enemyLocomotion.transform.position.y;

                ground.y = groundY;
                enemyAnimator.finishCombo = false;
                enemyLocomotion.navMeshAgent.enabled = false;
                enemyAnimator.ApplyTargetAnimation("MA12_C", true, false);
                enemyManger.currentRecoveryTime = coolDown;
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
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed * 10 / Time.deltaTime);
                #endregion
            }
            enemyLocomotion.enemyRigidbody.AddForce(Vector3.down * 10 * Time.deltaTime, ForceMode.VelocityChange);
            enemyLocomotion.transform.position += enemyAnimator.anim.deltaPosition;

            return this;

        }
    }
}
