using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyAnimator : DP_ParentAnimation
    {
        // Start is called before the first frame update
        DP_EnemyLocomotion enemyLocomotion;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyLocomotion = GetComponentInParent<DP_EnemyLocomotion>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLocomotion.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            // Vector3 velocity = deltaPosition / delta;
            // enemyLocomotion.enemyRigidbody.velocity = velocity * 10;
        }
        public void exitParryAnimation()
        {
            anim.SetBool("existParry",true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
