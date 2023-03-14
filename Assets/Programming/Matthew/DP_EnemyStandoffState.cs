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
        public float goalTime = 2f;

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
                // enemyAnimator.anim.SetBool("standoffDest", true);

                return nextState;
            }
            else if (!directionDetermined)
            {
                currentTime = 0;
                moveDir = Random.Range(0, 3);
                directionDetermined = true;
            }

            #region CirclePlayer
            // in range step back
            enemyLocomotion.navMeshAgent.enabled = true;
            enemyLocomotion.navMeshAgent.speed = 2.5f;
            if (enemyLocomotion.distanceFromtarget < enemyLocomotion.stoppingDistance && moveDir != 2)
            {

                enemyAnimator.anim.SetFloat("Vertical", -0.5f, 0.1f, Time.deltaTime);
                enemyAnimator.anim.SetFloat("phase2", 0f, 0.1f, Time.deltaTime);
                FacePlayer(enemyManger, enemyLocomotion);

                Vector3 fleeDirection = (enemyLocomotion.currentTarget.transform.position - this.transform.position).normalized;
                enemyLocomotion.navMeshAgent.SetDestination(this.transform.position - (fleeDirection * (enemyLocomotion.stoppingDistance + 1) / 2));
            }
            else if (moveDir == 2)
            {
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
                    enemyLocomotion.navMeshAgent.speed = 3.2f;
                    enemyLocomotion.enemyRigidbody.velocity = enemyLocomotion.navMeshAgent.velocity;
                }
                #endregion
            }
            else if (moveDir != 2)
            {
                Vector3 circleDirection = Vector3.Cross((this.transform.position - enemyLocomotion.currentTarget.transform.position),
                                                        (moveDir == 0 ? this.transform.up : -this.transform.up)).normalized;
                enemyAnimator.anim.SetFloat("Vertical", 0.5f, 0.2f, Time.deltaTime);
                enemyAnimator.anim.SetFloat("phase2", (moveDir == 0 ? 0.5f : -0.5f), 0.2f, Time.deltaTime);
                FacePlayer(enemyManger, enemyLocomotion);
                enemyLocomotion.navMeshAgent.SetDestination(this.transform.position - (circleDirection * (enemyLocomotion.stoppingDistance + 1)));
            }
            //out range circle 
            // after circle time return to combatstance
            #endregion
            currentTime += Time.deltaTime;
            // HandleStandoff(enemyManger, enemyLocomotion, enemyAnimator);
            return this;
        }
        private void FacePlayer(DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion)
        {
            Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            direction.y = 0;
            direction.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
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