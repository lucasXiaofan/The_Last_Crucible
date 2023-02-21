using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_PlayerInventory : MonoBehaviour
    {
        DP_WeaponSlotManager weaponSlotManager;
        DP_inputHandler inputHandler;
        public DP_WeaponItem leftWeapon;
        public DP_WeaponItem rightWeapon;
        public DP_WeaponItem unarmedWeapon;

        public DP_WeaponItem[] rightHandSlots = new DP_WeaponItem[1];
        public DP_WeaponItem[] leftHandSlots = new DP_WeaponItem[1];

        public List<DP_PickUpObjects> objectsInventory;
        public List<DP_FlaskItem> FlaskInventory;
        public List<DP_WeaponItem> weaponInventory;

        private int currentLeftWeaponIndex = -1;
        private int currentRightWeaponIndex = -1;


        private void Awake()
        {
            inputHandler = GetComponent<DP_inputHandler>();
            weaponSlotManager = GetComponentInChildren<DP_WeaponSlotManager>();
        }
        private void Start()
        {
            rightWeapon = unarmedWeapon;
            //leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            //weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);

        }

        public bool CheckHasKey()
        {
            for (int i = 0; i < objectsInventory.Count; i++)
            {
                if (objectsInventory[i].isKey)
                {

                    //objectsInventory.remove remove key;
                    return true;
                }
            }
            return false;
        }
        public void ChangeRightWeaponInSlot()
        {
            currentRightWeaponIndex++;
            if (currentRightWeaponIndex > rightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);

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
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);

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
