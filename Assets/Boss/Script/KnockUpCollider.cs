using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class KnockUpCollider : MonoBehaviour
    {
        [SerializeField] CapsuleCollider KpCollider;
        public int KnockUpDamage = 50;
        private void Start()
        {
            KpCollider = GetComponent<CapsuleCollider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                DP_animationHandler animationHandler = other.transform.GetComponentInChildren<DP_animationHandler>();
                DP_PlayerStats playerStats = other.transform.GetComponentInParent<DP_PlayerStats>();

                animationHandler.ApplyTargetAnimation("HitUp", true, false);
                playerStats.TakeDamage(10, true);
            }
        }
        public void enableKnockUp()
        {
            KpCollider.enabled = true;
        }
        public void disableKnockUp()
        {
            KpCollider.enabled = false;
        }
    }
}
