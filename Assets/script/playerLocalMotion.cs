using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{


    public class playerLocalMotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputhandler;
        Vector3 moveDirection;
        [HideInInspector]
        public Transform myTransfrom;
        [HideInInspector]
        public AnimatorHandler animatorHandler;
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed =5f;
        [SerializeField]
        float rotationSpeed =10f;
        [SerializeField]
        float sprintSpeed = 8f;
        public bool isSprint;
        private void Start() 
        {
            rigidbody = GetComponent<Rigidbody>();
            inputhandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransfrom = transform;
            animatorHandler.Initialize();
        }
        private void Update()
        {
            float delta = Time.deltaTime;
            isSprint = inputhandler.b_input;
            inputhandler.TickInput(delta);
            HandleMovement(delta);
            handleRollingAndSprinting(delta);


        }

        #region Movement
        Vector3 nV;
        Vector3 targetPosition;

        private void HandleRoataion(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputhandler.moveAmount;

            targetDir = cameraObject.forward*inputhandler.vertical;// Q1 what is targetDir do?
            targetDir += cameraObject.right*inputhandler.horizontal;

            targetDir.Normalize();//Q2what does normalize do? A: make the vector (x,y)/magnitude, 
            //magnitude 1
            targetDir.y = 0; //Q3 why making vector3.y to zero?
            if (targetDir == Vector3.zero)
                targetDir = myTransfrom.forward;
            float rs = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransfrom.rotation,tr,rs*delta);
            myTransfrom.rotation = targetRotation;

        }
        public void HandleMovement(float delta)
        {
            if(inputhandler.rollFlag)
                return;
            moveDirection = cameraObject.forward*inputhandler.vertical;
            moveDirection += cameraObject.right*inputhandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y =0;

            float speed = movementSpeed;
            
            if(inputhandler.sprintFlag)
            {
                speed = sprintSpeed;
                isSprint = true;
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speed;
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection,nV);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.updateAnimatorValues(inputhandler.moveAmount,0,isSprint);
            if (animatorHandler.CanRotate)
            {
                HandleRoataion(delta);
            }

        }
        
        public void handleRollingAndSprinting(float delta) 
        {
            if(animatorHandler.anim.GetBool("Interacted"))
            {
                
                return;
            }
            if (inputhandler.rollFlag)
            {
                
                moveDirection = cameraObject.forward*inputhandler.vertical;
                moveDirection += cameraObject.right*inputhandler.horizontal;
                if (inputhandler.moveAmount >0)
                {
                    animatorHandler.PlayerTargetAnimation("roll",true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransfrom.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayerTargetAnimation("StepBack", true);
                }
            }
            
            
                
            
        }
        #endregion
        
    }
}