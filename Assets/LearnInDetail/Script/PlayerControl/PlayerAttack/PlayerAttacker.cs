using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class PlayerAttacker : MonoBehaviour
    {
        // Start is called before the first frame update
        DP_animationHandler animationHandler;
        void Start()
        {
            animationHandler = GetComponentInChildren<DP_animationHandler>();
        }

        public void HandleLightAttack(DP_WeaponItem weaponItem)
        {
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_1, true);
        }
        public void HandleHeavyAttack(DP_WeaponItem weaponItem)
        {
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Heavy_Attack_1, true);
        }


    }
}
