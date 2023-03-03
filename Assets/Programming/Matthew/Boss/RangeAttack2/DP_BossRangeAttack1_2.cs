using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    //SWITCH TO ANIMATION EVENTS

    public class DP_BossRangeAttack1_2 : DP_State
    {
        //public DP_BossChaseState bossChaseState;
        public DP_EnemyIdleState tempState;

        public DP_EnemyAttackActions attackSO;
        public Transform attackOrigin;
        public GameObject projectilePrefab;
        private Quaternion targetRotation;

        private GameObject projectileObj;

        private bool start = true;
        public float jumpHeight;
        private bool CR_running = false;

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

            //Start coroutine
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
            

            /*
            //Jump and land motions are not smooth, a bit choppy.
            if (enemyManger.isPreformingAction)
            {
                return this;
            }

            //Start attack
            if (start)
            {
                enemyManger.currentAttack = attackSO;
                enemyManger.AttackTarget();
                start = false;
            }

            return this;
            */
        }

        //Jump function
        public void jumpMotion(DP_EnemyManger enemyManger, float time)
        {
            Vector3 startPosition = enemyManger.transform.position;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                enemyManger.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, jumpHeight, startPosition.z), Mathf.Pow((elapsedTime / time), 2.4f));

                elapsedTime += Time.deltaTime;
            }
        }

        //Land function
        public void landMotion(DP_EnemyManger enemyManger, float time)
        {
            Vector3 startPosition = enemyManger.transform.position;
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                enemyManger.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, 0f, startPosition.z), Mathf.Pow((elapsedTime / time), 2.4f));

                elapsedTime += Time.deltaTime;
            }

            enemyManger.transform.rotation = new Quaternion(0f, enemyManger.transform.rotation.y, 0f, enemyManger.transform.rotation.w);
        }

        
        IEnumerator generalCoroutine(DP_EnemyAnimator enemyAnimator, DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion)
        {
            CR_running = true;

            float startRotationX = enemyManger.transform.rotation.x;
            float startRotationZ = enemyManger.transform.rotation.z;

            yield return StartCoroutine(jumpLandHandler(enemyAnimator, enemyManger, enemyLocomotion, "RangeAttack1_2 Jump", true, 0.83f));

            yield return StartCoroutine(attackCoroutine(enemyAnimator));
            //yield return StartCoroutine(attackCoroutine(enemyAnimator));

            yield return StartCoroutine(jumpLandHandler(enemyAnimator, enemyManger, enemyLocomotion, "RangeAttack1_2 Land", false, 0.83f));

            enemyManger.transform.rotation = new Quaternion(0f, enemyManger.transform.rotation.y, 0f, enemyManger.transform.rotation.w);

            CR_running = false;
        }

        //bool jumpLand: true = jump, false = Land
        IEnumerator jumpLandHandler(DP_EnemyAnimator enemyAnimator, DP_EnemyManger enemyManger, DP_EnemyLocomotion enemyLocomotion, string animationName, bool jumpLand, float time)
        {
            Vector3 startPosition = enemyManger.transform.position;
            float elapsedTime = 0;
            float endHeight;

            if (jumpLand) { endHeight = jumpHeight; }
            else { endHeight = 0f; }

            enemyAnimator.ApplyTargetAnimation(animationName, true, true);

            while (elapsedTime < time)
            {
                enemyManger.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, endHeight, startPosition.z), Mathf.Pow((elapsedTime / time), 2.4f));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        //Animations can't loop into themselves?
        IEnumerator attackCoroutine(DP_EnemyAnimator enemyAnimator)
        {
            enemyAnimator.ApplyTargetAnimation("RangeAttack1_2 Swing", true, true);
            
            //frame 38
            yield return new WaitForSeconds(0.32f);
            ProjectileAttack(targetRotation);

            //frame 94
            yield return new WaitForSeconds(0.41f);
            ProjectileAttack(targetRotation);
            yield return new WaitForSeconds(0.4f);

            yield return new WaitForSeconds(0.8f);
        }
        

        public void ProjectileAttack(Quaternion targetRotation)
        {
            projectileObj = Instantiate(projectilePrefab, attackOrigin.position, targetRotation);
        }

        public Quaternion HandleRotation(DP_EnemyLocomotion enemyLocomotion, DP_EnemyManger enemyManger)
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
