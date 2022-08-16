using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_WeaponSlot : MonoBehaviour
    {
        public GameObject currentWeapon;
        public Transform parentOverride;
        public bool RightSlot;
        public bool leftSlot;
        private void DestoryWeapon()
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
        }
        private void UnloadWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }

        public void UploadWeapon(DP_WeaponItem weaponItem)
        {
            DestoryWeapon();
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }
            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                model.transform.localRotation = Quaternion.identity;
                model.transform.localPosition = Vector3.zero;
                model.transform.localScale = Vector3.one;
            }
            currentWeapon = model;

        }
    }
}
