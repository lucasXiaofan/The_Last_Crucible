using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStats : DP_CharacterStats
    {
        public DP_PlayerHealthBar playerHealthBar;
        DP_EnemyAnimator enemyAnimator;
        DP_EnemyManger enemyManger;


        void Start()
        {
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
            enemyManger = GetComponent<DP_EnemyManger>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            playerHealthBar.SetMaximumHeath(maximumHealth);
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }


        public void TakeDamage(int damage, bool normal)
        {
            if (enemyManger.isDead)
            {
                return;
            }
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            if (normal)
            {
                enemyAnimator.ApplyTargetAnimation("getHit", true, false);
                if (currentHealth <= 0)
                {
                    enemyManger.isDead = true;
                    print("enemy is dead!");
                    enemyAnimator.ApplyTargetAnimation("dead", true, false);


                }
            }
            if (currentHealth <= 0)
            {
                enemyManger.isDead = true;
            }


        }

    }
}

