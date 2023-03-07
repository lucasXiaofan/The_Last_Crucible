using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_EnemyManger : DP_Character
    {
        DP_EnemyLocomotion enemyLocomotion;
        DP_EnemyAnimator enemyAnimator;
        DP_EnemyStats enemyStats;
        public DP_State currentState;

        public bool isPreformingAction;
        public DP_EnemyAttackActions[] enemyAttacksList;
        public DP_EnemyAttackActions currentAttack;

        [Header("A.I Setting")]
        public float detectionRadius = 20f;
        public float minDetectionAngle = -60f;
        public float maxDetectionAngle = 60f;
        public float currentRecoveryTime = 0;

        [Header("Combat Setting")]
        public BoxCollider BackStabCollider;
        public Transform BackStabPoint;
        public bool isDead;
        public CapsuleCollider body;
        public Image LockonIcon;
        public BoxCollider ExecuteCollider;
        public Transform ExecutePoint;
        public bool Recovering;



        private void Awake()
        {
            enemyLocomotion = GetComponent<DP_EnemyLocomotion>();
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
            enemyStats = GetComponent<DP_EnemyStats>();
            body = GetComponent<CapsuleCollider>();

            LockonIcon.enabled = false;


        }
        private void Update()
        {
            enemyAnimator.anim.SetBool("isDead", isDead);
            if (isDead)
            {
                enemyStats.handleDeath();
                return;
            }


            HandleRoveryTimer();
        }

        private void FixedUpdate()
        {

            isPreformingAction = enemyAnimator.anim.GetBool("isInteracting");
            if (isDead)
            {

                enemyLocomotion.navMeshAgent.enabled = false;
                LockonIcon.enabled = false;
                return;
            }
            // make sure the distance is always updated
            if (!(enemyLocomotion.currentTarget == null))
            {
                enemyLocomotion.distanceFromtarget = Vector3.Distance(transform.position, enemyLocomotion.currentTarget.transform.position);
            }

            EnemyStateMachine();
            enemyStats.RecoverPosture();
        }
        private void LateUpdate()
        {


        }

        private void EnemyStateMachine()
        {

            if (currentState != null)
            {
                DP_State nextState = currentState.Tick(this, enemyStats, enemyAnimator, enemyLocomotion);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }

            }
        }
        private void SwitchToNextState(DP_State state)
        {
            currentState = state;
        }


        #region  Attacks

        private void HandleRoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
                Recovering = true;
            }
            if (Recovering)
            {
                if (currentRecoveryTime <= 0)
                {
                    Recovering = false;
                }
            }
        }
        public void AttackTarget()
        {

            if (Recovering || isPreformingAction)
            {
                currentAttack = null;
                return;
            }

            else if (currentAttack == null)
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
