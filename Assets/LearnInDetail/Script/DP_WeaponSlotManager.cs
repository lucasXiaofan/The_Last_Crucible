using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_WeaponSlotManager : MonoBehaviour
    {
        DP_WeaponSlot leftWeaponSlot;
        DP_WeaponSlot RightWeaponSlot;
        private void Awake()
        {
            DP_WeaponSlot[] weaponSlots = GetComponentsInChildren<DP_WeaponSlot>();
            foreach (DP_WeaponSlot weaponSlot in weaponSlots)
            {
                if (weaponSlot.leftSlot)
                {
                    leftWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.RightSlot)
                {
                    RightWeaponSlot = weaponSlot;
                }
            }
        }
        public void LoadWeaponOnSlot(DP_WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftWeaponSlot.UploadWeapon(weaponItem);
            }
            else
            {
                RightWeaponSlot.UploadWeapon(weaponItem);
            }
        }
    }
}