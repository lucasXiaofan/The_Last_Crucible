using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_input;
        public bool rollFlag;
        public float rollInputTimer;
        public bool sprintFlag;
        

        PlayerControl inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControl();
                inputActions.PlayerMovement.Movement.performed += inputactions => movementInput = inputactions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            }
            inputActions.Enable();

        }
        public void OnDisable()
        {
            inputActions.Disable();

        }
        public void TickInput(float delta)//why is this function even necessary?
        {
            Moveinput(delta);
            handleRollInput(delta);
        }
        public void Moveinput(float delta)//why is the delta necessary there?
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

        }

        private void handleRollInput(float delta)
        {
            b_input = UnityEngine.InputSystem.Keyboard.current.leftShiftKey.isPressed;//inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            //print(b_input);//inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed);
            if (b_input)
            {
                rollInputTimer += delta;
                sprintFlag = true;

                //print("rollFlag = "+rollFlag);

            }

            else
            {
                if (rollInputTimer > 0.0f && rollInputTimer < 0.3f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }

        }
    }

}
