using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DP
{
    public class DP_SingleEquipmentSlotFunctions : MonoBehaviour
    {
        public Image icon;
        DP_WeaponItem weaponItem;
        public bool RES1Selected;
        public bool RES2Selected;
        public bool LES1Selected;
        public bool LES2Selected;
        public void EquipItem(DP_WeaponItem newWeapon)
        {
            weaponItem = newWeapon;
            icon.sprite = newWeapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void UnequipItem()
        {
            weaponItem = null;
            icon.sprite = null;
            icon.enabled = false;
        }

    }
}