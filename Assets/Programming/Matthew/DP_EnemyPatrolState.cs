using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyPatrolState : DP_State
    {
        public DP_EnemyPursueState enemyPursueState;

        [Header("Enemy Patrol")]
        //New variables added:
        
        //Define preset points for enemy to patrol between.
        public GameObject[] patrolLocations = new GameObject[2];
        private float distanceFromPatrolLocation;
        private GameObject currentPatrolLocation;
        private int patrolLocationIndex;
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.detectionDistance)
            {
                return enemyPursueState;
            }
            HandlePatrolMovement(enemyManger,enemyLocomotion,enemyAnimator);
            return this;
        }
        //New functions
        public void HandlePatrolMovement(DP_EnemyManger enemyManger,
                                        DP_EnemyLocomotion enemyLocomotion,
                                        DP_EnemyAnimator enemyAnimator)
        {
            if (enemyManger.isPreformingAction)
            {
                return;
            }
            //For each point, have the enemy travel to it.
            //Once enemy reaches point, go to next one in list
            //I THINK THERES NO NEED TO DEFINE WHEN TO STOP PATROLING? HANDLED IN SM

            if (currentPatrolLocation == null)
            {
                patrolLocationIndex = 0;
                currentPatrolLocation = patrolLocations[patrolLocationIndex];
            }

            distanceFromPatrolLocation = Vector3.Distance(transform.position, currentPatrolLocation.transform.position);
            if (distanceFromPatrolLocation <= enemyLocomotion.stoppingDistance)
            {
                //Reassign to next patrol location
                //Handle end of array
                if (patrolLocationIndex == (patrolLocations.Length - 1))
                {
                    patrolLocationIndex = 0;
                }
                else
                {
                    patrolLocationIndex += 1;
                }

                currentPatrolLocation = patrolLocations[patrolLocationIndex];
            }

            //Handle movement
            // float distanceFromPatrolPoint = Vector3.Distance(transform.position, currentPatrolLocation.transform.position);
            Vector3 targetDirection = currentPatrolLocation.transform.position - transform.position;
            float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
            if (enemyManger.isPreformingAction)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                enemyLocomotion.navMeshAgent.enabled = false;
            }
            else
            {
                enemyLocomotion.navMeshAgent.enabled = true;
                enemyAnimator.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                enemyLocomotion.navMeshAgent.SetDestination(currentPatrolLocation.transform.position);
                enemyLocomotion.enemyRigidbody.velocity = enemyLocomotion.navMeshAgent.velocity;
            }
            HandleRotationTowardsPatrolLocation(enemyManger,enemyLocomotion);
        }

        public void HandleRotationTowardsPatrolLocation(DP_EnemyManger enemyManger,
                                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyManger.isPreformingAction)
            {
                Vector3 direction = currentPatrolLocation.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyLocomotion.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyLocomotion.enemyRigidbody.velocity;

                enemyLocomotion.navMeshAgent.enabled = true;

                transform.rotation = Quaternion.Slerp(transform.rotation, enemyLocomotion.navMeshAgent.transform.rotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
            }
        }
    }
}
