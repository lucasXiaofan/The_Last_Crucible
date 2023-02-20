using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DP
{
    public class DP_EnemyLocomotion : MonoBehaviour
    {
        DP_EnemyManger enemyManger;
        DP_EnemyAnimator enemyAnimator;
        public Rigidbody enemyRigidbody;
        public LayerMask detectionLayer;
        public DP_CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        [Header("Enemy Movement")]
        public float distanceFromtarget;
        public float stoppingDistance = 1.5f;
        public float RotationSpeed = 15f;

        // New variable
        public float detectionDistance = 10f;





        private void Awake()
        {
            enemyRigidbody = GetComponent<Rigidbody>();
            enemyManger = GetComponent<DP_EnemyManger>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        public void HandleDetection()
        {
            // 1.having a Collider[] that store all the detected player
            // 2. using physics.overlapSphere(position, radius, layer)
            /// 
            /// 3. for each detected object if character stats != null, 
            ///     currentCharacter = object
            //      1.calculate the targetdirection = targetposition - transform location
            ///     2.calculate the viewable angle using Vector3.Angle(with target, transform.?)
            ///     if view angle in range of maxview and minview
            ///     current target = currentCharacter
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManger.detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                DP_CharacterStats characterStats = colliders[i].transform.GetComponent<DP_CharacterStats>();
                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewAbleAngle < enemyManger.maxDetectionAngle && viewAbleAngle > enemyManger.minDetectionAngle)
                    {
                        currentTarget = characterStats;
                    }
                }
            }
        }
        public void HandleMovement()
        {
            if (enemyManger.isPreformingAction)
            {
                return;
            }

            distanceFromtarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
            if (enemyManger.isPreformingAction)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromtarget > stoppingDistance)
                {
                    navMeshAgent.enabled = true;
                    enemyAnimator.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                    enemyAnimator.anim.SetFloat("Archer", 0, 0.5f, Time.deltaTime);
                    navMeshAgent.SetDestination(currentTarget.transform.position);
                    enemyRigidbody.velocity = navMeshAgent.velocity;
                }
                else
                {
                    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }
            HandleRotationTowardsTarget();
            // navMeshAgent.transform.localPosition = Vector3.zero;
            // navMeshAgent.transform.localRotation = Quaternion.identity;
        }
        public void HandleRotationTowardsTarget()
        {
            if (enemyManger.isPreformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidbody.velocity;

                navMeshAgent.enabled = true;

                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, RotationSpeed / Time.deltaTime);
            }

            //rotate with pathfinding (navmesh)
        }


    }

}