using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerEffectsManager : MonoBehaviour
    {
        DP_PlayerStats playerStats;
        [Header("Flasks")]
        public int FlaskAmount;
        public int healAmount;

        [Header("UI")]
        DP_QuickSlotUI quickSlotUI;
        private void Awake()
        {
            playerStats = GetComponentInParent<DP_PlayerStats>();
            quickSlotUI = FindObjectOfType<DP_QuickSlotUI>();
        }
        private void Start()
        {
            quickSlotUI.FlaskIconHandler(FlaskAmount);
        }

        public void HealPlayer()
        {
            playerStats.Heal(healAmount);
            FlaskAmount -= 1;
            quickSlotUI.FlaskIconHandler(FlaskAmount);
        }
    }

}
