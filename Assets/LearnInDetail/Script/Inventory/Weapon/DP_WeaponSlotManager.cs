using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_WeaponSlotManager : MonoBehaviour
    {
        DP_WeaponSlot leftWeaponSlot;
        DP_WeaponSlot RightWeaponSlot;
        DP_DamageCollider leftDamageCollider;
        DP_DamageCollider rightDamageCollider;
        Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
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
                LoadLeftWeaponCollider();
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Left_arm_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left_arm_empty", 0.2f);
                }
            }
            else
            {
                RightWeaponSlot.UploadWeapon(weaponItem);
                LoadRightWeaponCollider();
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Right_arm_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right_arm_empty", 0.2f);
                }
            }
        }

        #region Open and close weapon collider
        private void LoadLeftWeaponCollider()
        {
            leftDamageCollider = leftWeaponSlot.currentWeapon.GetComponentInChildren<DP_DamageCollider>();
        }
        private void LoadRightWeaponCollider()
        {
            rightDamageCollider = RightWeaponSlot.currentWeapon.GetComponentInChildren<DP_DamageCollider>();
        }
        public void OpenLeftWeaponCollider()
        {
            leftDamageCollider.EnableDamage();
        }
        public void OpenRightWeaponCollider()
        {
            rightDamageCollider.EnableDamage();
        }
        public void CloseLeftWeaponCollider()
        {
            leftDamageCollider.DisableDamage();
        }
        public void CloseRightWeaponCollider()
        {
            rightDamageCollider.DisableDamage();
        }
        #endregion

    }
}