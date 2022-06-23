using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        playerLocalMotion playerLocalMotion;
        cameraHandler camerahandler;
        Animator anim;
        [Header("Player Flag")]
        public bool isGround;
        public bool isInAir;
        public bool isSprint;
        public bool Interacted;

        private void Awake()
        {
            camerahandler = cameraHandler.singleton;
        }
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocalMotion = GetComponent<playerLocalMotion>();
        }

        private void FixedUpdate() //difference between update and fixedupdate
        {
            float delta = Time.fixedDeltaTime;
            if (camerahandler != null)
            {
                camerahandler.FollowTarget(delta);
                camerahandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;

            Interacted = anim.GetBool("Interacted");

            inputHandler.TickInput(delta);
            playerLocalMotion.HandleMovement(delta);
            playerLocalMotion.handleRollingAndSprinting(delta);





        }
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rt_input = false;
            inputHandler.rb_input = false;
            isSprint = inputHandler.b_input;

        }
    }
}
