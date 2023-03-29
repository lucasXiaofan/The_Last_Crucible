using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_WeaponSlotManager : MonoBehaviour
    {
        DP_WeaponSlot leftWeaponSlot;
        DP_WeaponSlot RightWeaponSlot;

        DP_DamageCollider rightDamageCollider;
        public GameObject CriticalBloodVFX;
        Animator animator;
        DP_QuickSlotUI quickSlotUI;
        DP_PlayerStats playerStats;
        public DP_WeaponItem attackingWeapon;
        public Transform VFXPoint;
        private void Awake()
        {
            playerStats = GetComponentInParent<DP_PlayerStats>();
            animator = GetComponent<Animator>();
            quickSlotUI = FindObjectOfType<DP_QuickSlotUI>();
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
            if (quickSlotUI == null)
            {
                print("No quickslotUI");
                return;
            }
            quickSlotUI.IconHandler(false, weaponItem);

        }

        #region Open and close weapon collider
        private void PlayVFX()
        {
            GameObject blood = Instantiate(CriticalBloodVFX, VFXPoint.position, Quaternion.identity);
        }

        private void LoadRightWeaponCollider()
        {
            rightDamageCollider = RightWeaponSlot.currentWeapon.GetComponentInChildren<DP_DamageCollider>();
        }

        public void OpenRightWeaponCollider()
        {
            rightDamageCollider.EnableDamage();
        }

        public void BoostkDamage(int damageBoost)
        {
            rightDamageCollider.currentWeaponDamage = damageBoost;
        }

        public void normalDamage(int normal)
        {
            rightDamageCollider.currentWeaponDamage = normal;
        }

        public void CloseRightWeaponCollider()
        {
            rightDamageCollider.DisableDamage();
        }
        #endregion
        #region Handle weapon stamina cost

        #endregion
    }
}