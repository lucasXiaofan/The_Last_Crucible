using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_RangeAttackCollider : MonoBehaviour
    {
        public int currentProjectileDamage = 30;

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DP_PlayerStats playerStats = other.GetComponent<DP_PlayerStats>();
                if (playerStats != null)
                {
                    DP_PlayerManager playerManager = other.GetComponent<DP_PlayerManager>();
                    if (playerManager != null)
                    {
                        /*
                        if (playerManager.isParrying)
                        {
                            DP_EnemyAnimator enemyAnimator = GetComponentInParent<DP_EnemyAnimator>();
                            DP_EnemyStats enemyStats = GetComponentInParent<DP_EnemyStats>();
                            if (enemyAnimator != null)
                            {
                                enemyAnimator.anim.SetBool("Parryed", true);
                                enemyStats.DamagePosture(30);
                                return;
                            }
                        }
                        */

                        if (playerManager.isParrying)
                        {
                            return;
                        }
                    }

                    playerStats.TakeDamage(currentProjectileDamage);

                }
            }
            /*
            if (other.tag == "Enemy")
            {
                DP_EnemyStats enemyStats = other.GetComponent<DP_EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage, true);
                }
            }
            */
        }
    }
}
