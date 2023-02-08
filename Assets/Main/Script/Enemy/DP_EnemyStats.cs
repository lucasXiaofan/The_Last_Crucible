using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStats : DP_CharacterStats
    {
        public DP_PlayerHealthBar playerHealthBar;
        DP_EnemyAnimator enemyAnimator;
        

        void Start()
        {
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
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
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            if(normal)
            {
                enemyAnimator.ApplyTargetAnimation("getHit",true,false);
                if (currentHealth <= 0)
                {
                    enemyAnimator.ApplyTargetAnimation("dead",true,false);
                }
            }
            
            
        }

    }
}

