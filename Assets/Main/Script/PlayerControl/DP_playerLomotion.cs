using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_playerLomotion : MonoBehaviour
    {
        public Transform cameraPos;
        public Transform playerTransform;
        DP_inputHandler inputHandler;
        Vector3 targetDirection;
        public Vector3 MoveDirection;
        Vector3 normalVector;
        public Rigidbody playerRigidBody;
        public CapsuleCollider playerCollider;
        DP_animationHandler animationHandler;
        DP_PlayerManager playerManager;
        DP_CameraControl cameraControl;
        public float moveSpeed = 5f;
        float rotatingSpeed = 5f;
        public float sprintSpeed = 10f;
        public bool jumping;
        public float jumpDistance = 3f;

        [Header("Handle Falling")]
        [SerializeField]
        float startPointOfRayCast = 0.5f;
        [SerializeField]
        float minimumDistanceToFall = 1f;
        [SerializeField]
        float rayCastOffset = 0.2f;
        LayerMask ignoreLayer;
        float fallingSpeed = 200f;
        public float fallingTimer;



        void Start()
        {
            cameraPos = Camera.main.transform;
            playerTransform = transform;
            playerManager = GetComponent<DP_PlayerManager>();
            playerRigidBody = GetComponent<Rigidbody>();
            playerCollider = GetComponent<CapsuleCollider>();
            inputHandler = GetComponent<DP_inputHandler>();
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            cameraControl = FindObjectOfType<DP_CameraControl>();
            animationHandler.initialize();
            playerManager.isGrounded = true;
            ignoreLayer = ~(1 << 8 | 1 << 11);
        }

        // Update is called once per frame

        #region Movement
        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag)
            {
                return;
            }
            if (playerManager.isInteracting)
                return;

            MoveDirection = cameraPos.forward * inputHandler.vertical;
            MoveDirection += cameraPos.right * inputHandler.horizontal;
            MoveDirection.Normalize();

            MoveDirection.y = 0;
            //I am not using ProjectOnPlane here
            if (inputHandler.sprintFlag)
            {
                moveSpeed = sprintSpeed;
                playerManager.isSprinting = true;
            }
            else
            {
                moveSpeed = 5f;
            }
            if (inputHandler.lockOnFlag)
            {
                animationHandler.HandleAnimatorFloat(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);
            }
            else
            {
                animationHandler.HandleAnimatorFloat(inputHandler.moveAmount, 0, playerManager.isSprinting);
            }
            MoveDirection *= moveSpeed;

            Vector3 projectVelocity = Vector3.ProjectOnPlane(MoveDirection, normalVector);
            //print(projectVelocity);
            playerRigidBody.velocity = projectVelocity;
            if (animationHandler.canRotate)
            {
                HandleRotation(delta);
            }


        }
        public void HandleRotation(float delta)
        {
            //targetDirection = Vector3.zero;
            if (inputHandler.lockOnFlag)
            {
                print("player body control");
                if (inputHandler.sprintFlag == false && inputHandler.rollFlag == false)
                {
                    print("player body control, none springflag");
                    Vector3 rotateDirection = MoveDirection;
                    rotateDirection = cameraControl.currentLockOnTransform.LockOnTransform.position - cameraControl.cameraTransform.position;
                    rotateDirection.Normalize();
                    rotateDirection.y = 0;

                    Quaternion tr = Quaternion.LookRotation(rotateDirection);
                    Quaternion trBetter = Quaternion.Slerp(playerTransform.rotation, tr, rotatingSpeed * Time.deltaTime);
                    playerTransform.rotation = trBetter;
                }
                else
                {
                    print("else");
                    targetDirection = cameraPos.forward * inputHandler.vertical;
                    targetDirection += cameraPos.right * inputHandler.horizontal;
                    targetDirection.Normalize();
                    targetDirection.y = 0;
                    if (targetDirection == Vector3.zero)
                    {
                        targetDirection = playerTransform.forward;
                    }
                    Quaternion tR_lockon = Quaternion.LookRotation(targetDirection);
                    Quaternion targetRotation_lockOn = Quaternion.Slerp(playerTransform.rotation, tR_lockon, rotatingSpeed * delta);
                    playerTransform.rotation = targetRotation_lockOn;
                }

            }
            else
            {
                targetDirection = cameraPos.forward * inputHandler.vertical;
                targetDirection += cameraPos.right * inputHandler.horizontal;
                targetDirection.Normalize();
                targetDirection.y = 0;
                if (targetDirection == Vector3.zero)
                {
                    targetDirection = playerTransform.forward;
                }
                Quaternion tR = Quaternion.LookRotation(targetDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerTransform.rotation, tR, rotatingSpeed * delta);

                playerTransform.rotation = targetRotation;
            }
        }
        public void HandleRollingAndSprint(float delta)
        {
            if (animationHandler.anim.GetBool("isInteracting"))
                return;
            if (inputHandler.rollFlag)
            {
                MoveDirection = cameraPos.forward * inputHandler.vertical;
                MoveDirection += cameraPos.right * inputHandler.horizontal;
                if (inputHandler.moveAmount > 0)
                {
                    MoveDirection.y = 0;
                    animationHandler.ApplyTargetAnimation("Roll", true);
                    Quaternion dir = Quaternion.LookRotation(MoveDirection);
                    playerTransform.rotation = dir;
                }
                else
                {
                    animationHandler.ApplyTargetAnimation("backStep", true);
                }
            }
        }
        public void HandleFalling(float delta, Vector3 moveDirection)
        {

            //things to consider
            //1. determine isINair 
            // 2. isGrounded
            // 3. during falling play animation and disallow other interaction
            // 4. land

            playerManager.isGrounded = true;//???why?
            RaycastHit hit;
            Vector3 origin = playerTransform.position;
            origin.y += startPointOfRayCast;

            if (Physics.Raycast(origin, transform.forward, 0.4f, ignoreLayer))
            {
                moveDirection = Vector3.zero;
            }

            if (playerManager.isInAir)
            {
                playerRigidBody.AddForce(-Vector3.up * fallingSpeed);
                //add a kick off force below
                Vector3 kickDir = moveDirection;
                playerRigidBody.AddForce(kickDir * fallingSpeed / 3.5f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin += dir * rayCastOffset; // why times offset, what will happen if not times offset?
            Vector3 targetPosition = playerTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceToFall, Color.red, 0.1f, false);

            if (Physics.Raycast(origin, -Vector3.up, out hit, 2f, ignoreLayer))
            {
                // normalVector = hit.normal;
                // Vector3 tp = hit.point;
                // targetPosition.y = tp.y;
                playerRigidBody.AddForce(transform.up * (-20 * 2 * Time.deltaTime), ForceMode.VelocityChange);
                playerManager.isGrounded = true;
                if (playerManager.isInAir)
                {
                    if (fallingTimer > 0.5f)
                    {
                        //Debug.Log("you've been falling for: " + fallingTimer);
                        animationHandler.ApplyTargetAnimation("land", true);
                    }
                    else
                    {
                        animationHandler.ApplyTargetAnimation("Empty", false);
                        fallingTimer = 0;
                    }
                    playerManager.isInAir = false;
                }
                // playerTransform.position = targetPosition;
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }
                if (playerManager.isInAir == false)
                {
                    if (playerManager.isInteracting == false)
                    {
                        animationHandler.ApplyTargetAnimation("fall", true);
                    }

                    Vector3 normalVel = playerRigidBody.velocity;
                    normalVel.Normalize();
                    playerRigidBody.velocity = normalVel * (moveSpeed / 2);//why using the positive normalvel??
                    playerManager.isInAir = true;
                }
            }

        }
        public void HandlePlayerJump(bool pressJump)
        {
            //HandleFalling(delta, moveDirection);
            if (playerManager.isInteracting)
            {
                return;
            }

            if (pressJump && playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
                jumping = true;
                animationHandler.ApplyTargetAnimation("jump", false);
                if (inputHandler.moveAmount > 0)
                {
                    MoveDirection = cameraPos.forward * inputHandler.vertical;
                    MoveDirection += cameraPos.right * inputHandler.horizontal;

                    Quaternion rotation = Quaternion.LookRotation(MoveDirection);
                    rotation.x = 0;

                    playerTransform.rotation = rotation;
                }
                // var vel = playerRigidBody.velocity;
                // vel.y = 40f;
                // playerRigidBody.velocity = vel;
                playerRigidBody.AddForce(Vector3.up * 20, ForceMode.VelocityChange);



                // if (inputHandler.rb_input)
                // {
                //     animationHandler.ApplyTargetAnimation("jumpAttack", true);
                // }


                Vector3 tp = playerTransform.position;
                //playerRigidBody.AddForce(Vector3.up * fallingSpeed);
                // tp.y = playerTransform.position.y + jumpDistance;
                // playerTransform.position = Vector3.Lerp(playerTransform.position, tp, Time.deltaTime);

                // I don't fucking know why the addforce is not fucking working holy shit 
                //playerRigidBody.AddForce(-Vector3.down * fallingSpeed);
            }

        }



        #endregion
    }
}
