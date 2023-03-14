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
        public DP_PlayerSoundManager soundManager;
        Animator animator;
        [Header("Player UI")]
        public DP_AlertTextUI textUI;
        public GameObject alertTextObject;
        public GameObject itemTextObject;
        public GameObject tutorial;

        [Header("Player Status")]
        public bool isInteracting;
        public bool isJumping;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool canDoAirAttack;
        public bool Falling;
        public bool isDead;

        [Header("Combat settings")]
        public Transform CriticalStabPoint;
        public bool isParrying = false;
        public bool isRolling = false;

        [Header("Player Inventory")]
        LayerMask ItemPickLayer;

        [Header("Player Interact")]
        string interactString;
        public GameObject door;

        [Header("SceneManagement")]
        public CanvasGroup TransitionScene;
        public GameObject DeathText;
        public GameObject Menu;
        int sceneIndex;

        [Header("Spawn Location")]
        public SpawnManager spawnManager;

        private void Awake()
        {
            isDead = false;
            HideCursor();
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            cameraControl = FindObjectOfType<DP_CameraControl>();
            ItemPickLayer = (1 << 8 | 1 << 17);
            isGrounded = true;
            spawnManager = FindObjectOfType<SpawnManager>();

        }

        void Start()
        {
            playerStats = GetComponent<DP_PlayerStats>();
            playerLomotion = GetComponent<DP_playerLomotion>();
            animationHandler = GetComponent<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerInventory = GetComponent<DP_PlayerInventory>();
            soundManager = GetComponentInChildren<DP_PlayerSoundManager>();

            // UI 
            if (spawnManager != null)
                SpawnAtCheckpoint();
            StartCoroutine(TransitionFadeOut());
            itemTextObject.SetActive(false);
            tutorial.SetActive(true);
            textUI = FindObjectOfType<DP_AlertTextUI>();


        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                StartCoroutine(TransitionFadeIn());
                // SceneManager.LoadScene(sceneIndex);
                return;
            }
            float delta = Time.deltaTime;
            HandleDeath();
            canDoAirAttack = animator.GetBool("canDoAirAttack");
            canDoCombo = animator.GetBool("canDoCombo");
            isInteracting = animator.GetBool("isInteracting");
            animator.SetBool("IsInAir", isInAir);
            animator.SetBool("isGrounded", isGrounded);
            isRolling = animator.GetBool("isRolling");
            animator.SetBool("isFalling", Falling);
            inputHandler.TickInput(delta);
            playerLomotion.HandlePlayerJump(inputHandler.jump_input);
            playerLomotion.HandleRollingAndSprint(delta);
            CheckForInteractableObject();




            // If escape key, Cursor.visible = true; CursorLockMode off
        }
        private void FixedUpdate()
        {
            if (isDead)
            {
                return;
            }

            float delta = Time.deltaTime;

            playerLomotion.PlayerisGrounded();
            playerLomotion.HandleMovement(delta);
            playerLomotion.updateJumpTime(delta);
            playerStats.BloodEffect(delta);

            playerLomotion.HandleFalling(delta, playerLomotion.MoveDirection);

        }

        private void LateUpdate()
        {
            // if (playerStats.PlayerIsDead())
            // {
            //     return;
            // }
            float delta = Time.deltaTime;
            if (cameraControl != null)
            {
                cameraControl.FollowTarget(delta);
                cameraControl.CameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

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
            inputHandler.tab_input = false;
            inputHandler.heal_input = false;
            inputHandler.roll_backStep_input = false;



            isSprinting = inputHandler.roll_b_input;
            if (isInAir)
            {
                playerLomotion.fallingTimer += Time.deltaTime;
            }
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void HandleDeath()
        {
            if (playerStats.PlayerIsDead())
            {
                isDead = true;
                DeathText.SetActive(true);
                TransitionScene.gameObject.SetActive(true);
                TransitionScene.alpha = 0;
            }

        }
        IEnumerator TransitionFadeIn()
        {
            yield return new WaitForSeconds(1);
            while (TransitionScene.alpha < 1) // inicia o fade da transicao entre cenas
            {
                TransitionScene.alpha += 0.05f;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (spawnManager != null)
                SpawnAtCheckpoint();
        }

        IEnumerator TransitionFadeOut()
        {
            TransitionScene.gameObject.SetActive(true);
            DeathText.SetActive(false);
            TransitionScene.alpha = 1;
            while (TransitionScene.alpha > 0) // inicia o fade da transicao entre cenas
            {
                TransitionScene.alpha -= 0.05f;
                yield return new WaitForSeconds(0.1f);
            }

            TransitionScene.gameObject.SetActive(false);
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
                                if (door != null)
                                {
                                    door.GetComponent<Animator>().Play("Door");
                                    GameObject note = GameObject.Find("DoorTrigger");
                                    note.SetActive(false);

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

        public void LoadNextScene()
        {
            sceneIndex += 1;
            SceneManager.LoadScene(sceneIndex);
        }

        public void SpawnAtCheckpoint()
        {
            //move player to this position 
            print("called spawncheck");
            transform.position = new Vector3(spawnManager.spawnPosition.x - 2, spawnManager.spawnPosition.y, spawnManager.spawnPosition.z);
            print(transform.position);
        }
    }
}

