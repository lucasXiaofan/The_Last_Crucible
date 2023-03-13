using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyWeaponSlotManager : MonoBehaviour
    {

        DP_WeaponSlot rightHandSlot;
        DP_DamageCollider rightHandCollider;
        KnockUpCollider knockUpCollider;
        public DP_WeaponItem rightHandWeapon;
        void Awake()
        {
            DP_WeaponSlot[] weaponSlots = GetComponentsInChildren<DP_WeaponSlot>();
            foreach (DP_WeaponSlot weaponSlot in weaponSlots)
            {
                if (weaponSlot.RightSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }

        }
        void Start()
        {
            if (rightHandWeapon != null)
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
            knockUpCollider = rightHandSlot.currentWeapon.GetComponentInChildren<KnockUpCollider>();
            rightHandCollider = rightHandSlot.currentWeapon.GetComponentInChildren<DP_DamageCollider>();
            if (rightHandCollider != null)
            {
                disableCollider();
            }
            if (knockUpCollider != null)
            {
                CloseKnockUpCollider();
            }

            //print(rightHandSlot.currentWeapon);
        }

        public void openKnockUpCollider()
        {
            // if (rightHandCollider != null)
            // {
            //     rightHandCollider.DisableDamage();
            // }

            if (knockUpCollider != null)
            {
                knockUpCollider.enableKnockUp();
            }
        }
        public void CloseKnockUpCollider()
        {


            if (knockUpCollider != null)
            {
                knockUpCollider.disableKnockUp();
            }
        }

        public void openCollider()
        {
            if (rightHandCollider != null)
            {
                rightHandCollider.EnableDamage();
            }

        }
        public void disableCollider()
        {
            if (rightHandCollider != null)
            {
                rightHandCollider.DisableDamage();
            }

        }

    }
}
