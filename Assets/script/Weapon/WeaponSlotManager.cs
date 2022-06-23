using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot lefthandSlot;
        WeaponHolderSlot RighthandSlot;
        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponslot in weaponHolderSlots)
            {
                if (weaponslot.isLeftHandSlot)
                {
                    lefthandSlot = weaponslot;
                }
                else if (weaponslot.isRightHandSlot)
                {
                    RighthandSlot = weaponslot;
                }
            }
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                lefthandSlot.LoadWeaponModel(weaponItem);
            }
            else

            {
                RighthandSlot.LoadWeaponModel(weaponItem);
            }
        }
    }
}
