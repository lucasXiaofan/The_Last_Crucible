using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_playerLomotion : MonoBehaviour
    {
        DP_animationHandler animationHandler;
        DP_PlayerManager playerManager;
        DP_CameraControl cameraControl;
        DP_PlayerStats playerStats;
        DP_inputHandler inputHandler;

        public Transform cameraPos;
        public Transform playerTransform;
        Vector3 targetDirection;
        public Vector3 MoveDirection;
        Vector3 normalVector;
        public Rigidbody playerRigidBody;
        public CapsuleCollider playerCollider;

        [Header("Handle Movement")]
        public float accelerateTimer = 0.2f;
        public float moveSpeed = 5f;
        float rotatingSpeed = 9f;
        public float sprintSpeed = 10f;
        float previousHorizontal = 0f;

        [Header("Handle Falling")]
        [SerializeField]
        float ToGroundDistance;
        float groundMinDistance = 0.2f;
        [SerializeField]
        LayerMask ignoreLayer;
        public LayerMask ground;
        float fallingSpeed = 100f;
        public float fallingTimer;

        [Header("HandleJumpping")]
        float speedBeforeJump;
        Vector3 directionBeforeJump;



        void Start()
        {
            cameraPos = Camera.main.transform;
            playerTransform = transform;

            playerStats = GetComponent<DP_PlayerStats>();
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
            if (inputHandler.rollFlag || playerManager.isInteracting)
            {
                return;
            }

            #region Acceleration
            float difference = Mathf.Abs(previousHorizontal - inputHandler.horizontal);
            if (difference < 0.4f)
            {
                if (inputHandler.moveAmount > 0 && accelerateTimer <= 1)
                {
                    accelerateTimer += Time.deltaTime;
                }
                else if (inputHandler.moveAmount <= 0 && accelerateTimer > 0.2)
                {
                    accelerateTimer -= Time.deltaTime;
                }
            }
            else if ((playerManager.isJumping || playerManager.isSprinting) && difference >= 0.4f)
            {
                print("slow down");
                accelerateTimer = 0.5f;
            }

            #endregion

            if (playerManager.isJumping)
            {
                moveSpeed = speedBeforeJump;
            }
            else
            {
                if (inputHandler.sprintFlag && !playerManager.isJumping && !playerManager.isInAir)
                {
                    moveSpeed = sprintSpeed;
                    playerManager.isSprinting = true;
                }
                else
                {
                    moveSpeed = 5f;
                }


            }
            if (inputHandler.lockOnFlag)
            {
                animationHandler.HandleAnimatorFloat(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);
            }
            else
            {
                animationHandler.HandleAnimatorFloat(inputHandler.moveAmount, 0, playerManager.isSprinting);
            }

            MoveDirection = cameraPos.forward * inputHandler.vertical;
            MoveDirection += cameraPos.right * inputHandler.horizontal;
            MoveDirection.Normalize();
            MoveDirection.y = 0;
            // for acceleration change;
            previousHorizontal = inputHandler.horizontal;
            //
            MoveDirection *= moveSpeed * accelerateTimer;
            Vector3 projectVelocity = Vector3.ProjectOnPlane(MoveDirection, normalVector);
            playerRigidBody.velocity = projectVelocity;

            if (animationHandler.canRotate)
            {
                HandleRotation(delta);
            }



        }
        public void HandleRotation(float delta)
        {


            if (inputHandler.lockOnFlag)
            {

                if (inputHandler.sprintFlag == false && inputHandler.rollFlag == false)
                {
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
                    animationHandler.ApplyTargetAnimation("Roll", true, false);
                    Quaternion dir = Quaternion.LookRotation(MoveDirection);
                    playerTransform.rotation = dir;
                }

            }
            if (inputHandler.roll_backStep_input && inputHandler.moveAmount <= 0)
            {
                animationHandler.ApplyTargetAnimation("backStep", true, false);
            }
        }
        public void PlayerisGrounded()
        {

            if (playerManager.isJumping)
            {
                playerManager.isGrounded = false;
            }
            else
            {
                float groundDistance = CheckGroundDistance();
                playerManager.isGrounded = groundDistance <= groundMinDistance;

            }

        }
        public float CheckGroundDistance()
        {
            // radius of the SphereCast

            RaycastHit groundHit;
            float colliderHeight = 2f;

            float radius = 0.3f * 0.9f;
            var dist = 10f;
            // ray for RayCast
            Ray ray2 = new Ray(transform.position + new Vector3(0, colliderHeight / 2, 0), Vector3.down);
            // raycast for check the ground distance
            if (Physics.Raycast(ray2, out groundHit, (colliderHeight / 2) + dist, ground) && !groundHit.collider.isTrigger)
                dist = transform.position.y - groundHit.point.y;
            // sphere cast around the base of the capsule to check the ground distance
            if (dist >= groundMinDistance)
            {
                Vector3 pos = transform.position + Vector3.up * (radius);
                Ray ray = new Ray(pos, -Vector3.up);
                if (Physics.SphereCast(ray, radius, out groundHit, 0.5f, ground) && !groundHit.collider.isTrigger)
                {
                    // Physics.Linecast(groundHit.point + (Vector3.up * 0.1f), groundHit.point + Vector3.down * 0.15f, out groundHit, ignoreLayer);
                    float newDist = transform.position.y - groundHit.point.y;
                    if (dist > newDist) dist = newDist;
                }
            }
            ToGroundDistance = dist;
            return dist;
        }
        public void HandleFalling(float delta, Vector3 moveDirection)
        {

            if (playerManager.isJumping)
            {
                return;
            }
            // playerRigidBody.AddForce(-Vector3.up * fallingSpeed * (ToGroundDistance > 1.5f ? 3 : 3) * (fallingTimer + 1));
            if (playerManager.isGrounded)
            {

                playerManager.isInAir = false;
                fallingTimer = 0;
                Vector3 targetPositon = transform.position;
                Vector3 castOrigin = transform.position;
                castOrigin.y += 0.5f;
                RaycastHit hit;
                Physics.SphereCast(castOrigin, 0.25f, Vector3.down, out hit, ground);
                targetPositon.y = hit.point.y;
                transform.position = Vector3.Lerp(transform.position, targetPositon, Time.deltaTime / 0.1f);
                HandleRollingAndSprint(Time.deltaTime);

            }
            else
            {
                if (fallingTimer > 3f)
                {
                    playerStats.TakeDamage(500);
                }
                else if (playerManager.isInteracting == false
                && !playerManager.isJumping
                && ToGroundDistance > 1f
                )
                {
                    if (inputHandler.moveAmount <= 0)
                    {
                        animationHandler.ApplyTargetAnimation("fall", true, false);
                    }
                    else
                    {
                        animationHandler.ApplyTargetAnimation("fall", false, false);
                    }
                    playerManager.isInAir = true;
                }
            }
            if (playerManager.isInAir)
            {
                playerRigidBody.AddForce(-Vector3.up * fallingSpeed * 7 * (fallingTimer + 1));
                // //add a kick off force below
                Vector3 kickDir = moveDirection;
                playerRigidBody.AddForce(kickDir * fallingSpeed / 6f);
            }
            else if (!playerManager.isGrounded)
            {
                playerRigidBody.AddForce(-Vector3.up * fallingSpeed * 3);
            }

        }
        public void HandlePlayerJump(bool pressJump)
        {
            //HandleFalling(delta, moveDirection);
            if (playerManager.isInteracting || playerManager.isJumping)
            {
                return;
            }

            if (pressJump && playerManager.isGrounded)
            {
                speedBeforeJump = moveSpeed;
                directionBeforeJump = MoveDirection;
                playerManager.isGrounded = false;

                animationHandler.ApplyTargetAnimation("jump", false, true);
                if (inputHandler.moveAmount > 0)
                {
                    MoveDirection = cameraPos.forward * inputHandler.vertical;
                    MoveDirection += cameraPos.right * inputHandler.horizontal;

                    Quaternion rotation = Quaternion.LookRotation(MoveDirection);
                    rotation.x = 0;

                    playerTransform.rotation = rotation;
                }

                playerRigidBody.AddForce(Vector3.up * 40, ForceMode.VelocityChange);
            }

        }



        #endregion
    }
}
