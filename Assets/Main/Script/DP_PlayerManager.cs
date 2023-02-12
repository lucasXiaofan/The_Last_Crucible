using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_PlayerManager : DP_Character
    {
        DP_inputHandler inputHandler;
        DP_playerLomotion playerLomotion;
        DP_CameraControl cameraControl;
        DP_animationHandler animationHandler;
        Animator animator;
        public DP_AlertTextUI textUI;
        public GameObject alertTextObject;
        public GameObject itemTextObject;

        [Header("Player Status")]
        public bool isInteracting;
        public bool isJumping;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool canDoAirAttack;

        [Header("Combat settings")]
        public Transform CriticalStabPoint;
        public bool isParrying = false;

        private void Awake()
        {
            cameraControl = FindObjectOfType<DP_CameraControl>();
            isGrounded = true;
        }

        void Start()
        {
            playerLomotion = GetComponent<DP_playerLomotion>();
            // textUI = FindObjectOfType<DP_AlertTextUI>();
            animationHandler = GetComponent<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
            animator = GetComponentInChildren<Animator>();
            // itemTextObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            canDoAirAttack = animator.GetBool("canDoAirAttack");
            canDoCombo = animator.GetBool("canDoCombo");
            isInteracting = animator.GetBool("isInteracting");
            isJumping = animator.GetBool("isJumping");
            animator.SetBool("IsInAir", isInAir);
            animator.SetBool("isGrounded", isGrounded);
            inputHandler.TickInput(delta);
            playerLomotion.HandleRollingAndSprint(delta);
            CheckForInteractableObject();
            playerLomotion.HandlePlayerJump(inputHandler.jump_input);

            
            // If escape key, Cursor.visible = true; CursorLockMode off
        }
        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            playerLomotion.PlayerisGrounded();
            playerLomotion.HandleMovement(delta);
            playerLomotion.HandleFalling(delta, playerLomotion.MoveDirection);
        }

        private void LateUpdate()
        {
            float delta = Time.deltaTime;

            inputHandler.rollFlag = false;
            //inputHandler.sprintFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false; ///I don't know why enable them will disable the collider
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right = false;
            inputHandler.jump_input = false;
            inputHandler.menu_input = false;
            inputHandler.parry_input = false;

            if (cameraControl != null)
            {
                cameraControl.FollowTarget(delta);
                cameraControl.CameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }




            isSprinting = inputHandler.roll_b_input;
            if (isInAir)
            {
                playerLomotion.fallingTimer += Time.deltaTime;
            }
        }
        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.4f, transform.forward, out hit, 1f, cameraControl.ignoreLayers))
            {

                if (hit.collider.tag == "pickUpItem")
                {

                    DP_PickItem pickItem = hit.collider.GetComponent<DP_PickItem>();
                    if (pickItem != null)
                    {

                        string interactString = pickItem.ItemName;
//                        textUI.interactableText.text = interactString;
//                        alertTextObject.SetActive(true);
                        if (inputHandler.a_input)
                        {

                            hit.collider.GetComponent<DP_PickItem>().Interact(this);
                        }
                    }
                }
            }

            else
            {
                if (alertTextObject != null)
                {
                    alertTextObject.SetActive(false);
                }
                if (itemTextObject != null && inputHandler.a_input)
                {
                    itemTextObject.SetActive(false);
                }
            }
            inputHandler.a_input = false;
        }


    }
}
