using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DP
{
    public class Boss_CombatStanceState : DP_State
    {
        public DP_State[] RangeAttackStates;
        public DP_State PursueState;
        public DP_State StandOffState;
        public float walkSpeed = 3.2f;

        //after boss attacked player always return to this state to select next attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {

            if (enemyManger.Recovering && !enemyManger.isPreformingAction)
            {
                // return this;
                enemyAnimator.anim.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
                #region Rotation
                Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
                direction.y = 0;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
                #endregion

                #region walkTowardPlayer
                if (enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
                {
                    enemyAnimator.anim.SetFloat("Vertical", 0.5f, 0.1f, Time.deltaTime);
                    enemyLocomotion.navMeshAgent.enabled = true;
                    enemyLocomotion.navMeshAgent.SetDestination(enemyLocomotion.currentTarget.transform.position);
                    enemyLocomotion.navMeshAgent.speed = walkSpeed;
                    enemyLocomotion.enemyRigidbody.velocity = enemyLocomotion.navMeshAgent.velocity;
                }
                #endregion


            }
            else if (!enemyManger.Recovering && !enemyManger.isPreformingAction)
            {
                enemyLocomotion.navMeshAgent.enabled = false;
                // int doRangeAttack = Random.Range(0, 2);
                // print("do range attacks? " + doRangeAttack);
                // if (doRangeAttack == 0)
                // {
                //     int attack = Random.Range(0, RangeAttackStates.Length);
                //     return RangeAttackStates[attack];
                // }
                return PursueState;
            }
            return this;
            // return StandOffState;

        }
    }
}
