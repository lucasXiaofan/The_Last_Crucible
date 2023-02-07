using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP{
    public class DP_EnemyWeaponSlotManager : MonoBehaviour
    {
        
        DP_WeaponSlot rightHandSlot;
        DP_DamageCollider rightHandCollider;
        public DP_WeaponItem rightHandWeapon;
        void Awake()
       {
            DP_WeaponSlot[] weaponSlots = GetComponentsInChildren<DP_WeaponSlot>();
            foreach(DP_WeaponSlot weaponSlot in weaponSlots)
            {
                if(weaponSlot.RightSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }

       }
        void Start()
        {
            if (rightHandWeapon!= null)
            {
                LoadWeaponOnSlot(rightHandWeapon);
            }
        }
        public void LoadWeaponOnSlot(DP_WeaponItem weaponItem)
        {
                rightHandSlot.UploadWeapon(weaponItem);
                LoadWeaponsDamageCollider();
        }

        public void LoadWeaponsDamageCollider()
        {
                rightHandCollider = rightHandSlot.currentWeapon.GetComponentInChildren<DP_DamageCollider>();
                print(rightHandSlot.currentWeapon);
        }

        public void openCollider()
        {
            print("enabled");
            rightHandCollider.EnableDamage();
        }
        public void disableCollider()
        {
                rightHandCollider.DisableDamage();
        }
    }
}
