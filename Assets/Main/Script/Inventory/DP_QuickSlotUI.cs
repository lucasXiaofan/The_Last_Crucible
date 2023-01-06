using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DP
{
    public class DP_QuickSlotUI : MonoBehaviour
    {
        public Image leftIcon;
        public Image RightIcon;
        public void IconHandler(bool isleft, DP_WeaponItem weaponItem)
        {
            if (isleft)
            {
                if (weaponItem != null || weaponItem.itemIcon != null)
                {
                    leftIcon.sprite = weaponItem.itemIcon;
                    leftIcon.enabled = true;
                }
                else
                {
                    leftIcon = null;
                    leftIcon.enabled = false;
                }
            }
            else
            {
                if (weaponItem != null || weaponItem.itemIcon != null)
                {
                    RightIcon.sprite = weaponItem.itemIcon;
                    RightIcon.enabled = true;
                }
                else
                {
                    RightIcon = null;
                    RightIcon.enabled = false;
                }
            }
        }
    }
}
