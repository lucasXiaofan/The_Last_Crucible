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

        public void initialize()
        {
            anim = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");

        }
        public void HandleAnimatorFloat(float ver, float hori)
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
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

        }
        public void DoRotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }

    }
}
