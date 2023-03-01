using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerEffectsManager : MonoBehaviour
    {
        DP_PlayerStats playerStats;
        DP_PlayerManager playerManager;
        DP_animationHandler animationHandler;
        [Header("Flasks")]
        public int FlaskAmount;
        public int healAmount;

        [Header("UI")]
        DP_QuickSlotUI quickSlotUI;
        private void Awake()
        {
            playerStats = GetComponentInParent<DP_PlayerStats>();
            animationHandler = GetComponent<DP_animationHandler>();
            playerManager = GetComponentInParent<DP_PlayerManager>();
            quickSlotUI = FindObjectOfType<DP_QuickSlotUI>();
        }
        private void Start()
        {
            quickSlotUI.FlaskIconHandler(FlaskAmount);
        }

        public void HealPlayer()
        {
            if (!playerManager.isInteracting)
                animationHandler.ApplyTargetAnimation("drink_flask", true, false);

        }

        public void HealEffect()
        {
            playerStats.Heal(healAmount);
            FlaskAmount -= 1;
            quickSlotUI.FlaskIconHandler(FlaskAmount);
        }
    }

}
