using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_DamageCollider : MonoBehaviour
    {
        public int currentWeaponDamage = 30;
        Collider damageCollider;
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }
        public void EnableDamage()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamage()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
            {
                DP_PlayerStats playerStats = other.GetComponent<DP_PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }
            if (other.tag == "Enemy")
            {
                DP_EnemyStats enemyStats = other.GetComponent<DP_EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }

        }
    }
}