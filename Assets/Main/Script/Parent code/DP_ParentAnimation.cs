using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ParentAnimation : MonoBehaviour
    {
        public Animator anim;
        public void ApplyTargetAnimation(string animationName, bool isInteracting, bool isJumping)
        {
            anim.applyRootMotion = isInteracting;
            anim.applyRootMotion = isJumping;
            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("isJumping", isJumping);
            anim.CrossFade(animationName, 0.2f);
        }
    }
}
