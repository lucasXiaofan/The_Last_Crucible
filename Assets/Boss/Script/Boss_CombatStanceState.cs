using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DP
{
    public class Boss_CombatStanceState : DP_State
    {
        public DP_State RangeAttackState;
        public DP_State PursueState;
        //after boss attacked player always return to this state to select next attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            enemyLocomotion.navMeshAgent.enabled = true;
            if (enemyManger.Recovering)
            {
                // circling player
                #region Rotation
                Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
                direction.y = 0;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
                #endregion
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                
            }
            else
            {
                print("enter the pursue state");
                return PursueState;
            }
            return this;
            
        }
    }
}
