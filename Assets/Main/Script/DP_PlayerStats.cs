using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_PlayerStats : DP_CharacterStats
    {
        public DP_PlayerHealthBar playerHealthBar;
        public DP_PlayerHealthBar StaminaBar;
        DP_animationHandler animationHandler;
        DP_PlayerManager playerManager;
        public GameObject bloodVFX;
        public GameObject ParryVFX;

        //stamina related
        public int maxStamina;
        public int staminaLevel = 10;
        public int currentStamina;
        [Header(" Damaged BloodEffect")]
        public Image BloodEffectImg;
        public float duration = 1f;
        public float durationTimer;
        public float fadeSpeed = 10f;
        void Start()
        {
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            playerManager = GetComponentInParent<DP_PlayerManager>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            if (playerHealthBar == null || BloodEffectImg == null)
            {
                print("No playerHealthbar or bloodeffectimg");
                return;
            }
            playerHealthBar.SetMaximumHeath(maximumHealth);
            BloodEffectImg.color = new Color(BloodEffectImg.color.r, BloodEffectImg.color.g,
                BloodEffectImg.color.b, 0);
            //StaminaBar.SetMaximumHeath(maxStamina);
        }

        public void BloodEffect(float delta)
        {
            if (BloodEffectImg == null)
            {
                print("No get damaged effect image for player.");
                return;
            }
            if (BloodEffectImg.color.a > 0)
            {
                durationTimer += delta;
                if (durationTimer < duration)
                {
                    float TemPa = BloodEffectImg.color.a;
                    TemPa -= delta * fadeSpeed;
                    BloodEffectImg.color = new Color(BloodEffectImg.color.r, BloodEffectImg.color.g,
                    BloodEffectImg.color.b, TemPa);
                }

            }
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
        public void playBloodVFX(Vector3 location)
        {
            GameObject blood = Instantiate(bloodVFX, location, Quaternion.identity);
        }
        public void playerParryVFX(Vector3 location)
        {
            GameObject metalSpark = Instantiate(ParryVFX, location, Quaternion.identity);
        }

        public void TakeDamage(int damage, bool normal = false)
        {
            if (PlayerIsDead() || playerManager.isRolling)
            {
                return;
            }
            currentHealth = currentHealth - damage;
            durationTimer = 0;
            BloodEffectImg.color = new Color(BloodEffectImg.color.r, BloodEffectImg.color.g,
                BloodEffectImg.color.b, 1);
            playerHealthBar.SetHeathBarValue(currentHealth);
            if (!playerManager.isInteracting && !normal)
            {
                playerManager.soundManager.PlayRandomDamageSoundFX();
                animationHandler.ApplyTargetAnimation("playerHitReaction", true, false);
            }

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
        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            {
                if (currentHealth > maximumHealth)
                {
                    currentHealth = maximumHealth;
                }
            }
            playerHealthBar.SetHeathBarValue(currentHealth);
        }

    }
}
