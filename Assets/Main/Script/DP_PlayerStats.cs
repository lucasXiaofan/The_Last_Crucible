using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerStats : MonoBehaviour
    {
        public DP_PlayerHealthBar playerHealthBar;
        public DP_PlayerHealthBar StaminaBar;
        DP_animationHandler animationHandler;
        public int maximumHealth;
        public int currentHealth;
        public int healthLevel = 10;

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
            StaminaBar.SetMaximumHeath(maxStamina);
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
        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;
            StaminaBar.SetHeathBarValue(currentStamina);
        }
    }
}