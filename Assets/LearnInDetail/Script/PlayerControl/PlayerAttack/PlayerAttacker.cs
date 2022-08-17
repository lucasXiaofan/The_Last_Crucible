using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class PlayerAttacker : MonoBehaviour
    {
        // Start is called before the first frame update
        DP_animationHandler animationHandler;
        DP_inputHandler inputHandler;
        public string lastAttack;
        void Start()
        {
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
        }
        public void HandleCombo(DP_WeaponItem weaponItem)
        {
            if (inputHandler.comboFlag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weaponItem.Oh_Light_Attack_1)
                {
                    animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_2, true);
                }
            }
        }

        public void HandleLightAttack(DP_WeaponItem weaponItem)
        {
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Light_Attack_1, true);
            lastAttack = weaponItem.Oh_Light_Attack_1;
        }

        public void HandleHeavyAttack(DP_WeaponItem weaponItem)
        {
            animationHandler.ApplyTargetAnimation(weaponItem.Oh_Heavy_Attack_1, true);
            lastAttack = weaponItem.Oh_Heavy_Attack_1;
        }


    }
}
