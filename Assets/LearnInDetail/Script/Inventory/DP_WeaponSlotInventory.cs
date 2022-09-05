using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_WeaponSlotInventory : MonoBehaviour
    {
        public Image Icon;
        DP_WeaponItem item;
        public void AddItem(DP_WeaponItem newWeapon)
        {
            item = newWeapon;
            Icon.sprite = item.itemIcon;
            Icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void DeleteItem(DP_WeaponItem newWeapon)
        {
            item = null;
            Icon.sprite = null;
            Icon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
