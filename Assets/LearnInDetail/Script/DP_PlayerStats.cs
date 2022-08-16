using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerStats : MonoBehaviour
    {
        public DP_PlayerHealthBar playerHealthBar;
        DP_animationHandler animationHandler;
        public int maximumHealth;
        public int currentHealth;
        public int healthLevel = 10;
        void Start()
        {
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            playerHealthBar.SetMaximumHeath(maximumHealth);
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            animationHandler.ApplyTargetAnimation("playerHitReaction", true);
            if (currentHealth <= 0)
            {
                animationHandler.ApplyTargetAnimation("dead", true);
            }
        }




    }
}
