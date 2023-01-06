using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_WeaponPickUp : DP_PickItem
    {
        public DP_WeaponItem weapon;
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

            playerManager.itemTextObject.SetActive(true);
            playerManager.textUI.itemText.text = weapon.itemName;
            playerManager.textUI.itemIcon.sprite = weapon.itemIcon;
            playerLomotion.playerRigidBody.velocity = Vector3.zero;
            animationHandler.ApplyTargetAnimation("pickUp", true);
            inventory.weaponInventory.Add(weapon);
            Destroy(gameObject);

        }
    }
}
