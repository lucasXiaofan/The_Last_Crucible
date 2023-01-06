using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_EquipmentSlotFunctions : MonoBehaviour
    {
        public bool RES1Selected;
        public bool RES2Selected;
        public bool LES1Selected;
        public bool LES2Selected;
        DP_SingleEquipmentSlotFunctions[] equipmentArray;
        private void Start()
        {
            equipmentArray = GetComponentsInChildren<DP_SingleEquipmentSlotFunctions>();
        }
        public void LoadEquipmentIcon(DP_PlayerInventory inventory)
        {

            for (int i = 0; i < equipmentArray.Length; i++)
            {


                if (equipmentArray[i].RES1Selected)
                {

                    equipmentArray[i].EquipItem(inventory.rightHandSlots[0]);
                }
                if (equipmentArray[i].RES2Selected)
                {
                    equipmentArray[i].EquipItem(inventory.rightHandSlots[1]);
                }
                if (equipmentArray[i].LES1Selected)
                {
                    equipmentArray[i].EquipItem(inventory.leftHandSlots[0]);
                }
                if (equipmentArray[i].LES2Selected)
                {
                    equipmentArray[i].EquipItem(inventory.leftHandSlots[1]);
                }
            }
        }

        public void RightEquimentSlot_1_Selected()
        {
            RES1Selected = true;
        }
        public void RightEquimentSlot_2_Selected()
        {
            RES2Selected = true;
        }
        public void LeftEquimentSlot_1_Selected()
        {
            LES1Selected = true;
        }
        public void LeftEquimentSlot_2_Selected()
        {
            LES2Selected = true;
        }
    }
}