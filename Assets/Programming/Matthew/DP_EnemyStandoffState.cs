using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStandoffState : DP_State
    {
        //public DP_EnemyChaseState enemyChaseState;
        public DP_EnemyIdleState tempState;


        private bool destinationDetermined = false;
        public float destinationDistanceCheck = 1f;
        public float distanceMult = 1f;
        private Vector3 destinationLocation;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            //USE Common_StrafeWalkL_Root and Common_StrafeWalkR_Root ANIM
            if (enemyManger.isPreformingAction)
            {
                return this;
            }

            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }

            //Check if destination reached
            if (destinationDetermined)
            {
                float distanceFromDestination = Vector3.Distance(enemyManger.transform.position, destinationLocation);

                if (distanceFromDestination < destinationDistanceCheck)
                {
                    enemyLocomotion.navMeshAgent.updateRotation = true;
                    enemyLocomotion.navMeshAgent.enabled = false;

                    //Set animation bool to true
                    enemyAnimator.anim.SetBool("standoffDest", true);

                    return tempState;
                }
            }

            HandleStandoff(enemyManger, enemyLocomotion, enemyAnimator);
            return this;
        }

        private void HandleStandoff(DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion, DP_EnemyAnimator enemyAnimator)
        {
            //Determine Location
            if (!destinationDetermined)
            {
                //Determining Left(0)/Right(1)
                int moveDir = Random.Range(0, 2);
                if (moveDir == 0)
                {
                    destinationLocation = enemyManger.transform.position - (enemyManger.transform.right * distanceMult);
                    enemyAnimator.ApplyTargetAnimation("Standoff Left", true, false);
                }
                else if (moveDir == 1)
                {
                    destinationLocation = enemyManger.transform.position + (enemyManger.transform.right * distanceMult);
                    enemyAnimator.ApplyTargetAnimation("Standoff Right", true, false);
                }

                //Start NavMeshAgent
                enemyLocomotion.navMeshAgent.enabled = true;
                enemyLocomotion.navMeshAgent.updateRotation = false;
                enemyLocomotion.navMeshAgent.SetDestination(destinationLocation);

                destinationDetermined = true;
            }

            //Handle rotation towards player
            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);
        }
    }
}