using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_animationHandler : MonoBehaviour
    {
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;


        DP_inputHandler inputHandler;
        DP_playerLomotion playerLomotion;
        DP_PlayerManager playerManager;

        public void initialize()
        {
            inputHandler = GetComponentInParent<DP_inputHandler>();
            playerLomotion = GetComponentInParent<DP_playerLomotion>();
            playerManager = GetComponentInParent<DP_PlayerManager>();
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");

        }
        public void HandleAnimatorFloat(float ver, float hori, bool isSprint)
        {
            #region vertical
            float v = 0;
            if (ver > 0.55f)
            {
                v = 1;
            }
            else if (ver < 0.55f && ver > 0)
            {
                v = 0.5f;
            }
            else if (ver < -0.55f)
            {
                v = -1;
            }
            else if (ver > -0.55f && ver < 0)
            {
                v = -0.5f;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (hori > 0.55f)
            {
                h = 1;
            }
            else if (hori < 0.55f && hori > 0)
            {
                h = 0.5f;
            }
            else if (hori < -0.55f)
            {
                h = -1;
            }
            else if (hori > -0.55f && hori < 0)
            {
                h = -0.5f;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprint)
            {
                v = 2;
                h = hori;
            }
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

        }
        public void ApplyTargetAnimation(string animationName, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(animationName, 0.2f);
        }
        public void DoRotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }
        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;
            float delta = Time.deltaTime;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLomotion.playerRigidBody.drag = 0;
            //playerLomotion.playerRigidBody.velocity = velocity;
        }
        public void enableCombo()
        {
            anim.SetBool("canDoCombo", true);
            anim.SetBool("isInteracting", false);
        }
        public void disableCombo()
        {
            anim.SetBool("canDoCombo", false);
            //anim.SetBool("isInteracting", true);
        }
    }
}
