using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_DamagePlayer : MonoBehaviour
    {
        public int damage = 15;
        private void OnTriggerEnter(Collider other)
        {
            DP_PlayerStats playerStats = other.GetComponent<DP_PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                print("touched");
            }
        }
    }
}
