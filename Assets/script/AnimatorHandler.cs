using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        int vertical;
        int horizontal;
        public bool CanRotate;
        public InputHandler inputHandler;
        public playerLocalMotion playerLocalMotion;
        PlayerManager playerManager;



        public void Initialize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocalMotion = GetComponentInParent<playerLocalMotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void updateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {

            #region  Vertical
            float vi = 0.0f;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                vi = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                vi = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                vi = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                vi = -1;
            }
            else
            {
                vi = 0;
            }

            #endregion


            #region  Horizontal
            float h = 0.0f;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }

            #endregion
            if (isSprinting)
            {
                vi = 2;
                h = horizontalMovement;
            }
            anim.SetFloat(vertical, vi, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }
        public void PlayerTargetAnimation(string targetAmin, bool Interacted)
        {
            //print("call this animation function");
            //print("call this animation function");
            anim.applyRootMotion = Interacted;//applyrootmotion what is rootmotion
            anim.SetBool("Interacted", Interacted);
            anim.CrossFade(targetAmin, 0.2f);
        }
        public void CanRotateF()
        {
            CanRotate = true;
        }
        public void StopRotation()
        {
            CanRotate = false;
        }
        private void OnAnimatorMove()
        {
            if (playerManager.Interacted == false)
            {
                return;
            }
            float delta = Time.deltaTime;
            playerLocalMotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocalMotion.rigidbody.velocity = velocity;

        }
    }
}

