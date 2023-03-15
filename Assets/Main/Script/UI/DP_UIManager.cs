using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DP
{
    public class DP_UIManager : MonoBehaviour

    {
        DP_EquipmentSlotFunctions equipmentSlotFunctions;

        [Header("UI Window")]
        public GameObject hudWindows;//player health bar, quick slots. etc
        public GameObject playerUIBar;
        public GameObject weaponWindow;


        [Header("Weapon Inventory Window")]
        public DP_PlayerInventory playerInventory;
        public GameObject weaponSlotPrefab;
        public Transform weaponSlotParent;
        DP_WeaponSlotInventory[] weaponSlotInventories;
        private void Awake()
        {
            //equipmentSlotFunctions = FindObjectOfType<DP_EquipmentSlotFunctions>();
        }
        private void Start()
        {
            weaponSlotInventories = GetComponentsInChildren<DP_WeaponSlotInventory>(true);//this is the key
            //equipmentSlotFunctions.LoadEquipmentIcon(playerInventory);
            //allows to get children that is not activate.
        }
        public void UpdateUI()
        {
            for (int i = 0; i < playerInventory.weaponInventory.Count; i++)
            {
                #region  load weapon in weapon window
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
                #endregion

            }
        }
        public void TurnOnorOffUI(bool on)
        {
            playerUIBar.SetActive(on);
        }
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void ExistGame()
        {
            print("quit game");
            Application.Quit();
        }

    }
}

