using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_CameraControl : MonoBehaviour
    {
        private Transform myTransform;
        public Transform pivotTransform;
        public Transform cameraTransform; //??
        public Transform targetTransform;
        public static DP_CameraControl singleton;

        private LayerMask ignoreLayers;
        private Vector3 cameraTransformPoistion;
        private float targetPosition;// for camera collision

        public float rotationSpeed = 0.1f;
        public float pivotSpeed = 0.03f;
        public float followSpeed = 0.1f;

        public float defaultPositon;
        public float rotateAngle;
        public float pivotAngle;

        public float maximumPivot = 35f;
        public float minimumPivot = -35f;

        //camera collision
        public float miniOffet = 0.2f;
        public float CollisionSphereRadius = 0.2f;

        private Vector3 CameraVelocity = Vector3.zero;

        private void Awake()
        {
            myTransform = transform;
            singleton = this;
            ignoreLayers = ~(1 >> 8 | 1 >> 9 | 1 >> 10);
            Application.targetFrameRate = 60;
            defaultPositon = cameraTransform.localPosition.z;
            targetTransform = FindObjectOfType<DP_PlayerManager>().transform;

        }
        public void FollowTarget(float delta)
        {
            Vector3 targetP = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref CameraVelocity, delta / followSpeed);
            myTransform.position = targetP;
            HandleCollision(delta);

        }
        public void CameraRotation(float delta, float mouseX, float mouseY)
        {
            rotateAngle += (mouseX * rotationSpeed) / delta;
            Vector3 rotationH = Vector3.zero;
            rotationH.y = rotateAngle;
            Quaternion Lookdirection = Quaternion.Euler(rotationH);
            myTransform.rotation = Lookdirection;

            pivotAngle -= (mouseY * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
            rotationH = Vector3.zero;
            rotationH.x = pivotAngle;
            Quaternion pivotDirection = Quaternion.Euler(rotationH);
            pivotTransform.localRotation = pivotDirection;

        }
        private void HandleCollision(float delta)
        {
            targetPosition = defaultPositon;
            Vector3 dir = cameraTransform.position - pivotTransform.localPosition;
            dir.Normalize();
            RaycastHit hit;

            if (Physics.SphereCast(pivotTransform.position, CollisionSphereRadius,
                dir, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                float distance = Vector3.Distance(pivotTransform.position, hit.point);
                targetPosition = -(distance - miniOffet);

            }
            if (Mathf.Abs(targetPosition) < miniOffet)
            {
                targetPosition = -miniOffet;
            }
            float DampVelocity = 0f;

            cameraTransformPoistion.z = Mathf.SmoothDamp(cameraTransform.localPosition.z, targetPosition, ref DampVelocity, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPoistion;

        }

    }
}

