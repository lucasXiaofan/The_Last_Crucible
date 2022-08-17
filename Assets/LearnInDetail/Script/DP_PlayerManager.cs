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
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            isSprinting = inputHandler.roll_b_input;
            if (isInAir)
            {
                playerLomotion.fallingTimer += Time.deltaTime;
            }
        }
    }
}
