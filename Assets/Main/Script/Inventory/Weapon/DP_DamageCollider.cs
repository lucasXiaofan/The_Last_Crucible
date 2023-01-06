using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_DamageCollider : MonoBehaviour
    {
        DP_inputHandler inputHandler;
        public int currentWeaponDamage = 30;
        Collider damageCollider;
        private void Awake()
        {
            inputHandler = FindObjectOfType<DP_inputHandler>();
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
            //print("this doesn't cause the loop");
            damageCollider.enabled = false;
            // inputHandler.rb_input = false;
            // inputHandler.rt_input = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            //print(inputHandler.rb_input + " " + inputHandler.rt_input);
            if (other.tag == "Player")
            {
                DP_PlayerStats playerStats = other.GetComponent<DP_PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                    // if (inputHandler.rb_input)
                    // {
                    //     playerStats.TakeDamage(currentWeaponDamage);
                    // }
                    // else if (inputHandler.rt_input)
                    // {
                    //     playerStats.TakeDamage(currentWeaponDamage * 2);
                    // }

                }
            }
            if (other.tag == "Enemy")
            {
                DP_EnemyStats enemyStats = other.GetComponent<DP_EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                    // if (inputHandler.rb_input)
                    // {

                    // }
                    // else if (inputHandler.rt_input)
                    // {
                    //     enemyStats.TakeDamage(currentWeaponDamage * 2);
                    // }
                }
            }


        }
    }
}