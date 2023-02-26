using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_DamageCollider : MonoBehaviour
    {
        DP_inputHandler inputHandler;
        public int currentWeaponDamage = 30;
        BoxCollider damageCollider;
        private void Awake()
        {
            inputHandler = FindObjectOfType<DP_inputHandler>();
            damageCollider = GetComponent<BoxCollider>();
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
        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DP_PlayerStats playerStats = other.GetComponent<DP_PlayerStats>();
                if (playerStats != null)
                {
                    DP_PlayerManager playerManager = other.GetComponent<DP_PlayerManager>();
                    if(playerManager!= null)
                    {
                        if(playerManager.isParrying)
                        {
                            DP_EnemyAnimator enemyAnimator = GetComponentInParent<DP_EnemyAnimator>();
                            DP_EnemyStats enemyStats = GetComponentInParent<DP_EnemyStats>();
                            if (enemyAnimator!=null)
                            {
                                enemyAnimator.anim.SetBool("Parryed",true);
                                enemyStats.DamagePosture(30);
                                return;
                            }
                        }
                    }

                    playerStats.TakeDamage(currentWeaponDamage);

                }
            }
            if (other.tag == "Enemy")
            {
                DP_EnemyStats enemyStats = other.GetComponent<DP_EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage,true);
                }
            }


        }
    }
}