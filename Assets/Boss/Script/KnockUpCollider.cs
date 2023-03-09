using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class KnockUpCollider : MonoBehaviour
    {
        [SerializeField] CapsuleCollider KpCollider;
        DP_EnemyStats enemyStats;
        public int KnockUpDamage = 50;
        private void Start()
        {
            enemyStats = GetComponentInParent<DP_EnemyStats>();
            KpCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                DP_animationHandler animationHandler = other.transform.GetComponentInChildren<DP_animationHandler>();
                DP_PlayerStats playerStats = other.transform.GetComponentInParent<DP_PlayerStats>();
                DP_PlayerManager playerManager = other.transform.GetComponentInParent<DP_PlayerManager>();
                if (playerManager.isParrying)
                {
                    //player vfx
                    Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    playerStats.playerParryVFX(contactPoint);
                    enemyStats.DamagePosture(40);
                    return;
                }
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
