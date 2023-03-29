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

        // [Header("Combat vfx")]
        // public GameObject bloodvfx;

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
            if (quickSlotUI == null)
            {
                print("no quickslotUI implemented");
                return;
            }
            quickSlotUI.FlaskIconHandler(FlaskAmount);
        }
        // public void playBloodVFX(Vector3 location)
        // {
        //     GameObject blood = Instantiate(bloodvfx,location,Quaternion.identity);
        // }
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
