using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyAnimator : DP_ParentAnimation
    {
        // Start is called before the first frame update
        DP_EnemyLocomotion enemyLocomotion;
        DP_EnemyManger enemyManger;
        DP_EnemyStats enemyStats;
        public BoxCollider ExecutionCollider;
        public Transform rootPosition;
        private void Awake()
        {
            if (ExecutionCollider != null)
            {
                ExecutionCollider.enabled = false;
            }
            enemyStats = GetComponentInParent<DP_EnemyStats>();
            anim = GetComponent<Animator>();
            enemyLocomotion = GetComponentInParent<DP_EnemyLocomotion>();
            enemyManger = GetComponentInParent<DP_EnemyManger>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLocomotion.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            // deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            // enemyLocomotion.enemyRigidbody.velocity = velocity * 10;
            // enemyLocomotion.navMeshAgent.nextPosition = anim.rootPosition;
            // enemyLocomotion.transform.position = anim.rootPosition;


        }
        public void exitParryAnimation()
        {
            anim.SetBool("existParry", true);
        }

        public void ResetPosture()
        {
            enemyStats.currentPosture = 0;
            closeExecutionCollider();
        }

        public void HandleDeath()
        {
            if (enemyManger.isDead)
            {
                enemyManger.body.isTrigger = true;
                enemyManger.BackStabCollider.gameObject.SetActive(false);
                if (enemyManger.ExecuteCollider != null)
                {
                    ExecutionCollider.gameObject.SetActive(false);
                }
            }
        }

        // Update is called once per frame
        public void openExecutionCollider()
        {
            if (ExecutionCollider != null)
            {
                ExecutionCollider.enabled = true;
            }
        }

        public void closeExecutionCollider()
        {
            if (ExecutionCollider != null)
            {
                ExecutionCollider.enabled = false;
            }
        }
        // boss combat need to exist this for combo
        public void stopInteracting()
        {
            anim.SetBool("isInteracting",false);
        }

        #region combat

        #endregion
    }
}
