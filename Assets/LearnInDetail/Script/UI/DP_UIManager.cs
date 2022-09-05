using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_UIManager : MonoBehaviour
    {
        [Header("UI Window")]
        public GameObject hudWindows;//player health bar, quick slots. etc
        public GameObject playerUIBar;
        public GameObject weaponWindow;

        [Header("Weapon Inventory Window")]
        public DP_PlayerInventory playerInventory;
        public GameObject weaponSlotPrefab;
        public Transform weaponSlotParent;
        DP_WeaponSlotInventory[] weaponSlotInventories;
        private void Start()
        {
            weaponSlotInventories = GetComponentsInChildren<DP_WeaponSlotInventory>(true);//this is the key
            //allows to get children that is not activate.
        }
        public void UpdateUI()
        {
            for (int i = 0; i < playerInventory.weaponInventory.Count; i++)
            {
                print(weaponSlotInventories.Length);
                if (weaponSlotInventories.Length - 1 < i)
                {

                    Instantiate(weaponSlotPrefab, weaponSlotParent);
                    weaponSlotInventories = GetComponentsInChildren<DP_WeaponSlotInventory>(true);

                }
                if (weaponSlotInventories.Length <= playerInventory.weaponInventory.Count)
                {


                    weaponSlotInventories[i].AddItem(playerInventory.weaponInventory[i]);
                }
                else
                {
                    weaponSlotInventories[i].DeleteItem(playerInventory.weaponInventory[i]);
                }
            }
        }
        public void TurnOnorOffUI(bool on)
        {
            playerUIBar.SetActive(on);
        }
    }
}

