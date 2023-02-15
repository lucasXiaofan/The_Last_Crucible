using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_FlaskItem : ScriptableObject
    {
        [Header("Flask Type")]
        public bool healFlask;
        public int healAmount;
        public Image Icon;

        [Header("Recovery Amount")]
        public int healthRecoverAmount;

        [Header("Recovery FX")]
        public GameObject RecoveryFX;
        // public override void AttempToConsumeItem(DP_animationHandler animationHandler, DP_PlayerEffectsManager playerEffectsManager)
        // {
        //     base.AttempToConsumeItem(animationHandler, playerEffectsManager);
        //     playerEffectsManager.HealPlayer(healthRecoverAmount);
        // }
    }
}
