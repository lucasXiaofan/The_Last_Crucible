using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ItemPickUp : DP_PickItem
    {
        public DP_WeaponItem weapon;
        public DP_PickUpObjects objects;
        public override void Interact(DP_PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);
        }
        private void PickUpItem(DP_PlayerManager playerManager)
        {
            DP_playerLomotion playerLomotion;
            DP_animationHandler animationHandler;
            DP_PlayerInventory inventory;

            playerLomotion = playerManager.GetComponent<DP_playerLomotion>();
            animationHandler = playerManager.GetComponentInChildren<DP_animationHandler>();
            inventory = playerManager.GetComponent<DP_PlayerInventory>();


            if (weapon != null)
            {
                playerManager.textUI.itemText.text = weapon.itemName;
                playerManager.textUI.itemIcon.sprite = weapon.itemIcon;
                inventory.weaponInventory.Add(weapon);
            }
            else if (objects != null)
            {
                playerManager.textUI.itemText.text = objects.itemName;
                playerManager.textUI.itemIcon.sprite = objects.itemIcon;
                inventory.objectsInventory.Add(objects);
            }

            playerManager.itemTextObject.SetActive(true);
            playerLomotion.playerRigidBody.velocity = Vector3.zero;
            animationHandler.ApplyTargetAnimation("pickUp", true, false);
            Destroy(gameObject);

        }
    }
}
