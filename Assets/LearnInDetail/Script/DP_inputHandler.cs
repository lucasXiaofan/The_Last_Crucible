using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_inputHandler : MonoBehaviour
    {
        DP_PlayerControl inputActions;
        DP_CameraControl cameraControl;
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public float vertical;
        public float horizontal;

        Vector2 moveInput;
        Vector2 cameraInput;

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
        private void Awake()
        {
            cameraControl = DP_CameraControl.singleton;
        }
        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            if (cameraControl != null)
            {
                cameraControl.FollowTarget(delta);
                cameraControl.CameraRotation(delta, mouseX, mouseY);
            }

        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            MoveInputControl(delta);
        }
        private void MoveInputControl(float delta)
        {
            vertical = moveInput.y;
            horizontal = moveInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;


        }

    }


}



