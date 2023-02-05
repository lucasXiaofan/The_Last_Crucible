using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyManger : DP_Character
    {
        DP_EnemyLocomotion enemyLocomotion;
        DP_EnemyAnimator enemyAnimator;

        public bool isPreformingAction;
        public DP_EnemyAttackActions[] enemyAttacksList;
        public DP_EnemyAttackActions currentAttack;

        [Header("A.I Setting")]
        public float detectionRadius = 20f;
        public float minDetectionAngle = -60f;
        public float maxDetectionAngle = 60f;
        public float currentRecoveryTime = 0;

        private void Awake()
        {
            enemyLocomotion = GetComponent<DP_EnemyLocomotion>();
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
        }
        private void Update()
        {

            isPreformingAction = enemyAnimator.anim.GetBool("isInteracting");
            HandleRoveryTimer();
        }

        private void FixedUpdate()
        {
            HandleCurrentAction();
        }
        private void HandleCurrentAction()
        {
            if (enemyLocomotion.currentTarget != null)
            {
                enemyLocomotion.distanceFromtarget = Vector3.Distance(transform.position, enemyLocomotion.currentTarget.transform.position);
            }

            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }
            else if (enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
            {
                enemyLocomotion.HandleMovement();
            }
            else if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance)
            {
                AttackTarget();
            }




        }
        #region  Attacks
        private void HandleRoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }
        public void AttackTarget()
        {
            if (isPreformingAction)
            {
                return;
            }

            if (currentAttack == null)
            {
                SelectCurrentAttack();
            }
            else
            {
                currentRecoveryTime = currentAttack.recoveryTime;
                enemyAnimator.ApplyTargetAnimation(currentAttack.actionAnimation, true, false);
                currentAttack = null; //to get new attack;
            }
        }

        public void SelectCurrentAttack()
        {
            if (currentAttack != null)
            {
                return;
            }

            Vector3 targetDirection = enemyLocomotion.currentTarget.transform.position - transform.position;
            float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
            enemyLocomotion.distanceFromtarget = Vector3.Distance(enemyLocomotion.currentTarget.transform.position, transform.position);

            int AttackIndex = Random.Range(0, enemyAttacksList.Length);
            for (int i = 0; i < enemyAttacksList.Length; i++)
            {
                DP_EnemyAttackActions enemyAttackActions = enemyAttacksList[i];
                if (enemyLocomotion.distanceFromtarget <= enemyAttackActions.maxDistanceNeededToAttack
                && enemyLocomotion.distanceFromtarget >= enemyAttackActions.minDistanceNeededToAttack
                && viewAbleAngle <= enemyAttackActions.maxAttackAngle
                && viewAbleAngle >= enemyAttackActions.minAttackAngle)
                {
                    if (i == AttackIndex) // how we decide for random attack from enemy
                    {
                        currentAttack = enemyAttackActions;
                    }
                }
            }
        }

        #endregion
    }
}
