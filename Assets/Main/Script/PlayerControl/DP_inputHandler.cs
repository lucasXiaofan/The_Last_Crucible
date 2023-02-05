using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_inputHandler : MonoBehaviour
    {
        DP_PlayerControl inputActions;
        DP_CameraControl cameraControl;
        PlayerAttacker playerAttacker;
        DP_PlayerInventory playerInventory;
        DP_PlayerManager playerManager;
        DP_UIManager uIManager;
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public float vertical;
        public float horizontal;

        //player flags:
        public bool roll_b_input;
        public bool jump_input;
        public bool a_input; //item pick up
        public bool menu_input;
        public bool rb_input;
        public bool rt_input;
        public bool d_pad_down;
        public bool d_pad_up;
        public bool d_pad_left;
        public bool d_pad_right;
        public bool lock_on_input;
        public bool lock_left_input;
        public bool lock_right_input;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool menuFlag;
        public bool lockOnFlag;
        float holdCounter;

        //stamina bar
        public DP_PlayerHealthBar StaminaStatus;
        Vector2 moveInput;
        Vector2 cameraInput;

        private void Awake()
        {
            uIManager = FindObjectOfType<DP_UIManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<DP_PlayerInventory>();
            playerManager = GetComponent<DP_PlayerManager>();
            cameraControl = FindObjectOfType<DP_CameraControl>();
        }

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new DP_PlayerControl();
                inputActions.PlayerMovement.Movement.performed += inputActions => moveInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += inputActions => cameraInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerAction.Jump.performed += i => jump_input = true;
                inputActions.PlayerQuickSlot.DPadRight.performed += i => d_pad_right = true;
                inputActions.PlayerQuickSlot.DPadLeft.performed += i => d_pad_left = true;
                inputActions.PlayerAction.RB.performed += i => rb_input = true;
                inputActions.PlayerAction.RT.performed += i => rt_input = true;
                inputActions.PlayerQuickSlot.PickUp.performed += i => a_input = true;
                inputActions.PlayerQuickSlot.OpenMenu.performed += i => menu_input = true;
                inputActions.PlayerMovement.LockOnTarget.performed += i => lock_on_input = true;
                inputActions.PlayerMovement.LockOnLeft.performed += i => lock_left_input = true;
                inputActions.PlayerMovement.LockOnRight.performed += i => lock_right_input = true;

            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            MoveInputControl(delta);
            HandleRollingInput(delta);
            HandleAttack(delta);
            HandleQuickSlot();
            HandleOpenAndCloseMenuUI();
            HandleLockOnInput();
            HandleJumpInput();
        }
        private void MoveInputControl(float delta)
        {
            vertical = moveInput.y;
            horizontal = moveInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollingInput(float delta)
        {
            roll_b_input = UnityEngine.InputSystem.Keyboard.current.leftShiftKey.isPressed;
            //|| UnityEngine.InputSystem.Gamepad.current.buttonEast.isPressed;
            sprintFlag = roll_b_input;
            if (roll_b_input)
            {
                holdCounter += delta;
                if (moveAmount > 0)
                {

                }
                else
                {
                    rollFlag = true;
                }

            }
            else
            {
                if (holdCounter > 0 && holdCounter < 0.5f)
                {
                    rollFlag = true;
                    sprintFlag = false;
                }
                holdCounter = 0;
            }
        }
        private void HandleAttack(float delta)
        {

            if (playerManager.isInteracting)
                return;
            if (rb_input) //&& StaminaStatus.alive())
            {
                if (playerManager.canDoAirAttack)
                {
                    playerAttacker.HandleAirAttack(playerInventory.rightWeapon);
                }
                else if (playerManager.canDoCombo && !playerManager.isJumping)
                {
                    comboFlag = true;
                    playerAttacker.HandleCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else if (!playerManager.isJumping)
                {
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }

            }
            if (rt_input) //&& StaminaStatus.alive())
            {
                print("Looping");
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }


        }
        private void HandleQuickSlot()
        {

            if (d_pad_left)
            {
                playerInventory.ChangeLeftWeaponInSlot();
            }
            if (d_pad_right)
            {
                playerInventory.ChangeRightWeaponInSlot();
            }
        }
        private void HandleOpenAndCloseMenuUI()
        {

            //print(menu_input);
            if (menu_input)
            {
                menuFlag = !menuFlag;
                uIManager.TurnOnorOffUI(menuFlag);
                uIManager.hudWindows.SetActive(!menuFlag);
                if (menuFlag)
                {

                    uIManager.UpdateUI();
                }
                else if (!menuFlag)
                {
                    uIManager.weaponWindow.SetActive(false);
                }
            }



        }
        private void HandleLockOnInput()
        {

            if (lock_on_input && lockOnFlag == false)
            {

                //cameraControl.ClearLockOn();
                lock_on_input = false;
                cameraControl.HandleLockOn();
                if (cameraControl.nearestLockTransform != null)
                {
                    cameraControl.currentLockOnTransform = cameraControl.nearestLockTransform;
                    lockOnFlag = true;
                }
            }
            else if (lock_on_input && lockOnFlag)
            {

                lock_on_input = false;
                lockOnFlag = false;
                cameraControl.ClearLockOn();
            }
            if (lockOnFlag && lock_left_input)
            {
                lock_left_input = false;

                cameraControl.HandleLockOn();
                if (cameraControl.leftLockOnTarget != null)
                {
                    cameraControl.currentLockOnTransform = cameraControl.leftLockOnTarget;
                }
            }
            if (lockOnFlag && lock_right_input)
            {
                lock_right_input = false;

                cameraControl.HandleLockOn();
                if (cameraControl.rightLockOnTarget != null)
                {
                    cameraControl.currentLockOnTransform = cameraControl.rightLockOnTarget;
                }
            }
        }

        private void HandleJumpInput()
        {
            inputActions.PlayerAction.Jump.performed += i => jump_input = true;
        }
    }


}



