using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_BossRangeAttack1_1 : DP_State
    {
        //public DP_BossChaseState bossChaseState;
        public DP_EnemyIdleState tempState;

        public DP_EnemyAttackActions attackSO;
        public Transform attackOrigin;
        public GameObject projectilePrefab;
        private Quaternion targetRotation;

        GameObject projectileObj;

        private bool start = true;
        private bool CR_running = false;

        //Need to time out animation with projectile spawn
        public override DP_State Tick(DP_EnemyManger enemyManger,
                                        DP_EnemyStats enemyStats,
                                        DP_EnemyAnimator enemyAnimator,
                                        DP_EnemyLocomotion enemyLocomotion)
        {
            targetRotation = HandleRotation(enemyLocomotion, enemyManger);

            if (enemyManger.isPreformingAction)
            {
                return this;
            }

            if (start)
            {
                StartCoroutine(generalCoroutine(enemyAnimator, enemyManger, enemyLocomotion));
                start = false;
                return this;
            }
            else
            {
                if (!CR_running)
                {
                    return tempState;
                }
            }
            return this;
        }

        IEnumerator generalCoroutine(DP_EnemyAnimator enemyAnimator, DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion)
        {
            CR_running = true;

            yield return StartCoroutine(attackCoroutine(enemyAnimator));
            yield return StartCoroutine(attackCoroutine(enemyAnimator));

            yield return StartCoroutine(rollCoroutine(enemyAnimator, enemyLocomotion));

            yield return StartCoroutine(attackCoroutine(enemyAnimator));
            yield return StartCoroutine(attackCoroutine(enemyAnimator));

            yield return null;

            CR_running = false;
        }

        IEnumerator attackCoroutine(DP_EnemyAnimator enemyAnimator)
        {
            enemyAnimator.ApplyTargetAnimation("", true, false);

            yield return new WaitForSeconds(0.5f);
            ProjectileAttack(targetRotation);
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator rollCoroutine(DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            //Determine move position
            //Determine left or right?

            //Play roll animation
            enemyAnimator.ApplyTargetAnimation("", true, false);
            yield return new WaitForSeconds(0.5f);
        }

        private void ProjectileAttack(Quaternion targetRotation)
        {
            projectileObj = Instantiate(projectilePrefab, attackOrigin.position, targetRotation);
        }

        private Quaternion HandleRotation(DP_EnemyLocomotion enemyLocomotion, DP_EnemyManger enemyManger)
        {
            if (enemyLocomotion.currentTarget == null)
            {
                enemyLocomotion.HandleDetection();
            }

            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);
            return targetDir;
        }
    }
}
