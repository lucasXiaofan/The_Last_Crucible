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

        public float rotationSpeed = 0.1f;
        public float pivotSpeed = 0.03f;
        public float followSpeed = 0.1f;

        public float defaultPositon;
        public float rotateAngle;
        public float pivotAngle;

        public float maximumPivot = 35f;
        public float minimumPivot = -35f;

        public Vector3 CameraVelocity;

        private void Awake()
        {
            myTransform = transform;
            singleton = this;
            ignoreLayers = ~(1 >> 8 | 1 >> 9 | 1 >> 10);
            Application.targetFrameRate = 60;
            defaultPositon = cameraTransform.localPosition.z;

        }
        public void FollowTarget(float delta)
        {
            Vector3 targetP = Vector3.Lerp(myTransform.position, targetTransform.position, followSpeed / delta);
            myTransform.position = targetP;

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

    }
}

