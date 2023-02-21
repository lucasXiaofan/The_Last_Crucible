using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ConsumableItem : DP_Item
    {
        [Header("Item Quantity")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Animations")]
        public string consumeAnimation;

        public virtual void AttempToConsumeItem(DP_animationHandler animationHandler, DP_PlayerEffectsManager playerEffectsManager)
        {
            if (currentItemAmount > 0)
            {
                animationHandler.ApplyTargetAnimation(consumeAnimation, true, false);
            }

        }
    }
}
