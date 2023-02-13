using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerStats : DP_CharacterStats
    {
        public DP_PlayerHealthBar playerHealthBar;
        public DP_PlayerHealthBar StaminaBar;
        DP_animationHandler animationHandler;


        //stamina related
        public int maxStamina;
        public int staminaLevel = 10;
        public int currentStamina;
        void Start()
        {
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            playerHealthBar.SetMaximumHeath(maximumHealth);

            maxStamina = SetMaximumStaminaLevel();
            currentStamina = maxStamina;
            //StaminaBar.SetMaximumHeath(maxStamina);
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }
        private int SetMaximumStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        public bool PlayerIsDead()
        {
            return currentHealth <= 0;
        }

        public void TakeDamage(int damage)
        {
            if (PlayerIsDead())
            {
                return;
            }
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            animationHandler.ApplyTargetAnimation("playerHitReaction", true, false);
            if (currentHealth <= 0)
            {
                animationHandler.ApplyTargetAnimation("dead", true, false);
            }
        }
        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;
            //            StaminaBar.SetHeathBarValue(currentStamina);
        }
    }
}
