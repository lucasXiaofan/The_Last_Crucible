using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStandoffState : DP_State
    {
        //public DP_EnemyChaseState enemyChaseState;
        public DP_State nextState;

        private bool directionDetermined = false;
        public float distanceMult = 1f;
        private Vector3 destinationLocation;

        public float goalDistance = 3f;
        public float movementSpeed = 1f;
        public float goalTime = 1f;

        private float oldMovementSpeed;
        private int moveDir;
        private float currentTime = 0f;


        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            //USE Common_StrafeWalkL_Root and Common_StrafeWalkR_Root ANIM

            /*
            if (enemyManger.isPreformingAction)
            {
                return this;
            }
            */
            

            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }

            //Check if destination reached
            if (directionDetermined && currentTime >= goalTime)
            {
                enemyLocomotion.navMeshAgent.updateRotation = true;
                enemyLocomotion.navMeshAgent.enabled = false;
                enemyLocomotion.navMeshAgent.speed = oldMovementSpeed;

                //Set animation bool to true
                enemyAnimator.anim.SetBool("standoffDest", true);

                return nextState;
            }

            HandleStandoff(enemyManger, enemyLocomotion, enemyAnimator);
            return this;
        }

        private void HandleStandoff(DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion, DP_EnemyAnimator enemyAnimator)
        {
            //Determine Location
            if (!directionDetermined)
            {
                currentTime = 0f;
                enemyAnimator.anim.SetBool("standoffDest", false);

                //Determining Left(0)/Right(1)
                moveDir = Random.Range(0, 2);
                if (moveDir == 0)
                {
                    enemyAnimator.ApplyTargetAnimation("Standoff Left", true, false);
                }
                else if (moveDir == 1)
                {
                    enemyAnimator.ApplyTargetAnimation("Standoff Right", true, false);
                }

                //Start NavMeshAgent
                enemyLocomotion.navMeshAgent.enabled = true;
                enemyLocomotion.navMeshAgent.updateRotation = false;
                oldMovementSpeed = enemyLocomotion.navMeshAgent.speed;
                enemyLocomotion.navMeshAgent.speed = movementSpeed;

                directionDetermined = true;
            }

            //Manage direction
            //Move left
            if (moveDir == 0)
            {
                destinationLocation = enemyManger.transform.position - (enemyManger.transform.right * distanceMult);
            }
            //Move right
            else if (moveDir == 1)
            {
                destinationLocation = enemyManger.transform.position + (enemyManger.transform.right * distanceMult);
            }

            //Manage distance from player
            //Move forwards
            float distanceFromtarget = Vector3.Distance(transform.position, enemyLocomotion.currentTarget.transform.position);
            if (distanceFromtarget > goalDistance)
            {
                destinationLocation += (enemyManger.transform.forward * distanceMult);
            }
            //Move backwards
            else if (enemyLocomotion.distanceFromtarget < goalDistance)
            {
                destinationLocation -= (enemyManger.transform.forward * distanceMult);
            }

            

            //Handle rotation towards player
            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);


            enemyLocomotion.navMeshAgent.SetDestination(destinationLocation);
            currentTime += Time.deltaTime;
        }
    }
}