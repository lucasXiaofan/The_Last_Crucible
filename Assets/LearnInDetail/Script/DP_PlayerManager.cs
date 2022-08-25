using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_PlayerManager : MonoBehaviour
    {
        DP_inputHandler inputHandler;
        DP_playerLomotion playerLomotion;
        DP_CameraControl cameraControl;
        DP_animationHandler animationHandler;
        Animator animator;
        [Header("Player Status")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        private void Awake()
        {
            cameraControl = FindObjectOfType<DP_CameraControl>();
        }

        void Start()
        {
            playerLomotion = GetComponent<DP_playerLomotion>();

            animationHandler = GetComponent<DP_animationHandler>();
            inputHandler = GetComponent<DP_inputHandler>();
            animator = GetComponentInChildren<Animator>();
        }
        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            if (cameraControl != null)
            {
                cameraControl.FollowTarget(delta);
                cameraControl.CameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

        }
        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;

            canDoCombo = animator.GetBool("canDoCombo");
            isInteracting = animator.GetBool("isInteracting");
            inputHandler.TickInput(delta);
            playerLomotion.HandleMovement(delta);
            playerLomotion.HandleRollingAndSprint(delta);
            playerLomotion.HandleFalling(delta, playerLomotion.MoveDirection);
            CheckForInteractableObject();

        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            // inputHandler.rb_input = false;
            // inputHandler.rt_input = false;
            inputHandler.d_pad_down = false;
            inputHandler.d_pad_up = false;
            inputHandler.d_pad_left = false;
            inputHandler.d_pad_right = false;

            isSprinting = inputHandler.roll_b_input;
            if (isInAir)
            {
                playerLomotion.fallingTimer += Time.deltaTime;
            }
        }
        public void CheckForInteractableObject()
        {
            RaycastHit hit;
            print(inputHandler.a_input);
            if (Physics.SphereCast(transform.position, 0.4f, transform.forward, out hit, 1f, cameraControl.ignoreLayers))
            {
                print("touched");
                if (hit.collider.tag == "pickUpItem")
                {
                    print("is pick up item");
                    DP_PickItem pickItem = hit.collider.GetComponent<DP_PickItem>();
                    if (pickItem != null)
                    {
                        print("not null " + inputHandler.a_input);
                        string interactString = pickItem.ItemName;
                        if (inputHandler.a_input)
                        {
                            print("interacted");
                            hit.collider.GetComponent<DP_PickItem>().Interact(this);
                        }
                    }
                }
            }
            inputHandler.a_input = false;
        }

    }
}
