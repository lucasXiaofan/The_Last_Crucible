using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_inputHandler : MonoBehaviour
    {
        DP_PlayerControl inputActions;
        DP_CameraControl cameraControl;
        DP_animationHandler animationHandler;
        PlayerAttacker playerAttacker;
        DP_PlayerInventory playerInventory;
        DP_PlayerManager playerManager;
        DP_playerLomotion playerLomotion;
        DP_UIManager uIManager;
        DP_PlayerEffectsManager effectsManager;
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
        public bool parry_input;
        public bool heal_input;
        public bool roll_backStep_input;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool menuFlag;
        public bool lockOnFlag;
        public bool jumpFlag;

        float holdCounter;

        [Header("UI input")]
        public bool tab_input;
        private bool isUIOpen = true;

        //stamina bar
        public DP_PlayerHealthBar StaminaStatus;
        Vector2 moveInput;
        Vector2 cameraInput;

        // Handle Parry
        private bool isRight = true;

        private void Awake()
        {
            uIManager = FindObjectOfType<DP_UIManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<DP_PlayerInventory>();
            playerManager = GetComponent<DP_PlayerManager>();
            cameraControl = FindObjectOfType<DP_CameraControl>();
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            effectsManager = GetComponentInChildren<DP_PlayerEffectsManager>();
            playerLomotion = GetComponent<DP_playerLomotion>();
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
                inputActions.PlayerAction.Parry.performed += i => parry_input = true;
                inputActions.PlayerQuickSlot.Tutorial.performed += i => tab_input = true;
                inputActions.PlayerQuickSlot.Flask.performed += i => heal_input = true;
                inputActions.PlayerAction.Roll.performed += i => roll_backStep_input = true;

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
            //HandleQuickSlot();
            HandleOpenAndCloseMenuUI();
            HandleLockOnInput();
            HandleJumpInput();
            HandleParry();
            HandleTutorial();
            HandleHeal();
        }
        private void HandleParry()
        {
            if (playerManager.isInteracting || playerManager.isJumping) return;
            if (parry_input)
            {
                if (isRight)
                {
                    animationHandler.ApplyTargetAnimation("ParryR", true, false);
                }
                else
                {
                    animationHandler.ApplyTargetAnimation("ParryL", true, false);
                }
                isRight = !isRight;
            }
        }
        private void MoveInputControl(float delta)
        {
            vertical = moveInput.y;
            horizontal = moveInput.x;
            // _direction.y = 0;
            // _direction.x = Mathf.Clamp(moveInput.x, -1f, 1f);
            // _direction.z = Mathf.Clamp(moveInput.y, -1f, 1f);
            // if (_direction.magnitude > 1f)
            //     _direction.Normalize();
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
            horizontal = Mathf.Clamp(moveInput.x, -1f, 1f);

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollingInput(float delta)
        {
            if (moveAmount > 0)
            {
                roll_b_input = UnityEngine.InputSystem.Keyboard.current.leftShiftKey.isPressed;//|| (UnityEngine.InputSystem.Gamepad.current == null) ? false : UnityEngine.InputSystem.Gamepad.current.buttonEast.isPressed;

            }
            else
            {
                roll_b_input = false;
            }
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
            if (playerAttacker.CanExecute())
            {
                playerManager.canDoCombo = false;
            }
            if (playerManager.isInteracting && !playerManager.canDoCombo)
                return;
            if (rb_input) //&& StaminaStatus.alive())
            {
                if (playerAttacker.CanExecute())
                {
                    print("triggered execution");
                    playerAttacker.HandleExecution(true);
                }
                else if (playerAttacker.CanBackStab() && !playerManager.canDoCombo)
                {
                    print("why back stab");
                    playerAttacker.HandleExecution(false);
                }
                else if (playerManager.isJumping || playerManager.Falling)
                {
                    playerManager.Falling = true;
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
                playerManager.ShowCursor();
                // uIManager.hudWindows.SetActive(!menuFlag);
                if (menuFlag)
                {

                    //uIManager.UpdateUI();
                }
                else if (!menuFlag)
                {
                    playerManager.HideCursor();
                    uIManager.weaponWindow.SetActive(false);
                }
            }



        }
        private void HandleLockOnInput()
        {
            if (cameraControl.nearestLockTransform != null && cameraControl.nearestLockTransform.isDead)
            {
                cameraControl.currentLockOnTransform.LockonIcon.enabled = false;
                cameraControl.currentLockOnTransform = null;

                cameraControl.ClearLockOn();
                lockOnFlag = false;
                return;
            }

            if (lock_on_input && lockOnFlag == false)
            {

                //cameraControl.ClearLockOn();
                lock_on_input = false;
                cameraControl.HandleLockOn();
                if (cameraControl.nearestLockTransform != null)
                {

                    cameraControl.currentLockOnTransform = cameraControl.nearestLockTransform;
                    cameraControl.currentLockOnTransform.LockonIcon.enabled = true;
                    lockOnFlag = true;
                }
            }
            else if (lock_on_input && lockOnFlag)
            {

                lock_on_input = false;
                lockOnFlag = false;
                if (cameraControl.currentLockOnTransform != null)
                {
                    cameraControl.currentLockOnTransform.LockonIcon.enabled = false;
                }
                cameraControl.ClearLockOn();
            }
            if (lockOnFlag && lock_left_input)
            {
                lock_left_input = false;

                cameraControl.HandleLockOn();
                if (cameraControl.leftLockOnTarget != null)
                {
                    if (cameraControl.currentLockOnTransform != null)
                    {
                        cameraControl.currentLockOnTransform.LockonIcon.enabled = false;
                    }
                    cameraControl.currentLockOnTransform = cameraControl.leftLockOnTarget;
                    cameraControl.currentLockOnTransform.LockonIcon.enabled = true;
                }
            }
            if (lockOnFlag && lock_right_input)
            {
                lock_right_input = false;

                cameraControl.HandleLockOn();
                if (cameraControl.rightLockOnTarget != null)
                {
                    if (cameraControl.currentLockOnTransform != null)
                    {
                        cameraControl.currentLockOnTransform.LockonIcon.enabled = false;
                    }
                    cameraControl.currentLockOnTransform = cameraControl.rightLockOnTarget;
                    cameraControl.currentLockOnTransform.LockonIcon.enabled = true;
                }
            }
        }

        private void HandleJumpInput()
        {
            if (jump_input)
            {
                jumpFlag = true;
            }
        }
        public void HandleTutorial()
        {
            if (tab_input)
            {
                playerManager.tutorial.SetActive(true);
                playerManager.ShowCursor();
                // if (!isUIOpen)
                // {
                //     playerManager.tutorial.SetActive(true);
                // }
                // else
                // {
                //     playerManager.tutorial.SetActive(false);
                // }
                // isUIOpen = !isUIOpen;

            }
        }

        public void HandleHeal()
        {
            if (heal_input)
            {
                if (effectsManager.FlaskAmount > 0)
                {
                    effectsManager.HealPlayer();
                }
            }
        }
    }


}



