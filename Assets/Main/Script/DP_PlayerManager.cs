using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DP
{
    public class DP_PlayerManager : DP_Character
    {
        DP_inputHandler inputHandler;
        DP_playerLomotion playerLomotion;
        DP_CameraControl cameraControl;
        DP_animationHandler animationHandler;
        DP_PlayerInventory playerInventory;
        DP_PlayerStats playerStats;
        Animator animator;
        [Header("Player UI")]
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

        [Header("Player Inventory")]
        LayerMask ItemPickLayer;

        [Header("Player Interact")]
        string interactString;
        public GameObject door;
        bool interacted = false;

        [Header("SceneManagement")]
        int sceneIndex;

        private void Awake()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            cameraControl = FindObjectOfType<DP_CameraControl>();
            ItemPickLayer = (1 << 8 | 1 << 17);
            isGrounded = true;
        }

        void Start()
        {
            playerStats = GetComponent<DP_PlayerStats>();
            playerLomotion = GetComponent<DP_playerLomotion>();
            animationHandler = GetComponent<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerInventory = GetComponent<DP_PlayerInventory>();

            // UI 
            itemTextObject.SetActive(false);
            textUI = FindObjectOfType<DP_AlertTextUI>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (playerStats.PlayerIsDead())
            {
                SceneManager.LoadScene(sceneIndex);
                return;
            }
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
            // if (playerStats.PlayerIsDead())
            // {
            //     return;
            // }
            float delta = Time.deltaTime;
            playerLomotion.PlayerisGrounded();
            playerLomotion.HandleMovement(delta);
            playerLomotion.HandleFalling(delta, playerLomotion.MoveDirection);
        }

        private void LateUpdate()
        {
            // if (playerStats.PlayerIsDead())
            // {
            //     return;
            // }
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
            //RaycastHit hit;
            Collider[] items = Physics.OverlapSphere(transform.position, 0.3f, ItemPickLayer);
            //if (Physics.SphereCast(transform.position, 2f, transform.forward, out hit, 0.1f, ItemPickLayer))
            if (items.Length != 0)
            {
                itemTextObject.SetActive(false);

                if (items[0].tag == "pickUpItem")
                {

                    DP_PickItem pickItem = items[0].GetComponent<DP_PickItem>();
                    if (pickItem != null)
                    {
                        interactString = pickItem.ItemName;
                        textUI.interactableText.text = interactString;
                        alertTextObject.SetActive(true);
                        if (inputHandler.a_input)
                        {
                            items[0].GetComponent<DP_PickItem>().Interact(this);
                        }
                    }
                }
                else if (items[0].tag == "Trigger")
                {
                    DP_DoorOpener doorOpener = items[0].GetComponent<DP_DoorOpener>();
                    if (doorOpener != null)
                    {
                        if (playerInventory.CheckHasKey())
                        {
                            interactString = doorOpener.SuccessMessage;
                        }
                        else
                        {
                            interactString = doorOpener.FailMessage;
                        }

                        if (inputHandler.a_input)
                        {
                            if (playerInventory.CheckHasKey())
                            {
                                // play animation open door
                                // delete below later
                                if (door != null)
                                {
                                    door.SetActive(false);
                                }
                            }
                        }
                    }
                    textUI.interactableText.text = interactString;
                    alertTextObject.SetActive(true);
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

