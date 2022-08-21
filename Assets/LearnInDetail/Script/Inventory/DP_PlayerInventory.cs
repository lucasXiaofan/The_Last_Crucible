using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_PlayerInventory : MonoBehaviour
    {
        DP_WeaponSlotManager weaponSlotManager;
        public DP_WeaponItem leftWeapon;
        public DP_WeaponItem rightWeapon;
        public DP_WeaponItem unarmedWeapon;

        public DP_WeaponItem[] rightHandSlots = new DP_WeaponItem[1];
        public DP_WeaponItem[] leftHandSlots = new DP_WeaponItem[1];

        private int currentLeftWeaponIndex = -1;
        private int currentRightWeaponIndex = -1;


        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<DP_WeaponSlotManager>();
        }
        private void Start()
        {
            rightWeapon = unarmedWeapon;
            leftWeapon = unarmedWeapon;

        }
        public void ChangeRightWeaponInSlot()
        {
            currentRightWeaponIndex++;
            if (currentRightWeaponIndex > rightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);

            }
            else if (rightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = rightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            }
            else if (rightHandSlots[currentRightWeaponIndex] == null)
            {
                currentRightWeaponIndex++;
            }
        }
        public void ChangeLeftWeaponInSlot()
        {
            currentLeftWeaponIndex++;
            if (currentLeftWeaponIndex > leftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);

            }
            else if (leftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = leftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            }
            else if (leftHandSlots[currentLeftWeaponIndex] == null)
            {
                currentLeftWeaponIndex++;
            }
        }
    }
}
