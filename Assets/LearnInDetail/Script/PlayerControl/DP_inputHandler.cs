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
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public float vertical;
        public float horizontal;

        //player flags:
        public bool roll_b_input;
        public bool rb_input;
        public bool rt_input;
        public bool rollFlag;
        public bool sprintFlag;
        float holdCounter;

        Vector2 moveInput;
        Vector2 cameraInput;
        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<DP_PlayerInventory>();
            playerManager = GetComponent<DP_PlayerManager>();
        }

        private void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new DP_PlayerControl();
                inputActions.PlayerMovement.Movement.performed += inputActions => moveInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += inputActions => cameraInput = inputActions.ReadValue<Vector2>();
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
            if (roll_b_input)
            {
                holdCounter += delta;
                if (moveAmount > 0)
                {
                    sprintFlag = true;
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
            inputActions.PlayerAction.RB.performed += i => rb_input = true;
            inputActions.PlayerAction.RT.performed += i => rt_input = true;
            if (playerManager.isInteracting)
                return;
            if (rb_input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
            if (rt_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
    }


}



