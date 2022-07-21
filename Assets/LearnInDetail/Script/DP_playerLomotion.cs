using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_playerLomotion : MonoBehaviour
    {
        Transform cameraPos;
        Transform playerTransform;
        DP_inputHandler inputHandler;
        Vector3 targetDirection;
        Vector3 MoveDirection;
        Vector3 normalVector;
        Rigidbody playerRigidBody;
        DP_animationHandler animationHandler;
        float moveSpeed = 5f;
        float rotatingSpeed = 5f;
        void Start()
        {
            cameraPos = Camera.main.transform;
            playerTransform = transform;
            playerRigidBody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<DP_inputHandler>();
            animationHandler = GetComponentInChildren<DP_animationHandler>();
            animationHandler.initialize();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;

            HandleMovement(delta);
            if (animationHandler.canRotate)
            {
                HandleRotation(delta);
            }
            animationHandler.HandleAnimatorFloat(inputHandler.moveAmount, 0);

        }
        #region Movement
        private void HandleMovement(float delta)
        {
            inputHandler.TickInput(delta);
            MoveDirection = cameraPos.forward * inputHandler.vertical;
            MoveDirection += cameraPos.right * inputHandler.horizontal;
            MoveDirection.Normalize();

            MoveDirection.y = 0;
            //I am not using ProjectOnPlane here

            MoveDirection *= moveSpeed;

            Vector3 projectVelocity = Vector3.ProjectOnPlane(MoveDirection, normalVector);
            //print(projectVelocity);
            playerRigidBody.velocity = projectVelocity;


        }
        private void HandleRotation(float delta)
        {
            //targetDirection = Vector3.zero;
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
        #endregion
    }
}
