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

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<DP_WeaponSlotManager>();
        }
        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }
    }
}
