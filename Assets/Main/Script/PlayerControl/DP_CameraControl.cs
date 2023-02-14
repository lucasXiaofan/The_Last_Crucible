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

        public LayerMask ignoreLayers;
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

        //for lock on 
        float maximumTargetDistance = 30f;
        List<DP_EnemyManger> availableTarget = new List<DP_EnemyManger>();
        public DP_EnemyManger nearestLockTransform;
        public DP_EnemyManger currentLockOnTransform;
        public DP_EnemyManger leftLockOnTarget;
        public DP_EnemyManger rightLockOnTarget;
        DP_inputHandler inputHandler;

        private void Awake()
        {
            myTransform = transform;
            singleton = this;
            ignoreLayers = ~(1 >> 8 | 1 >> 9 | 1 >> 10);
            Application.targetFrameRate = 60;
            defaultPositon = cameraTransform.localPosition.z;
            targetTransform = FindObjectOfType<DP_PlayerManager>().transform;
            inputHandler = FindObjectOfType<DP_inputHandler>();

        }
        public void FollowTarget(float delta)
        {
            Vector3 targetP = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref CameraVelocity, delta / followSpeed);
            myTransform.position = targetP;
            HandleCollision(delta);

        }
        public void CameraRotation(float delta, float mouseX, float mouseY)
        {
            if (inputHandler.lockOnFlag == false && currentLockOnTransform == null)
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
            else
            {
                
                Vector3 dir = currentLockOnTransform.LockOnTransform.position - cameraTransform.position;
                dir.Normalize();
                dir.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTransform.LockOnTransform.position - pivotTransform.position;
                dir.Normalize();
                targetRotation = Quaternion.LookRotation(dir);
                // I don't understand the eularAngle?? what is the difference between eular angle 
                //and the Rotation
                // this will be really wonky pivotTransform.localRotation = targetRotation;
                Vector3 eularAngle = targetRotation.eulerAngles;
                eularAngle.y = 0;
                eularAngle.z = 0;
                pivotTransform.localEulerAngles = eularAngle;
            }

        }
        private void HandleCollision(float delta)
        {
            targetPosition = defaultPositon;
            Vector3 dir = cameraTransform.position - pivotTransform.position;
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

        public void HandleLockOn()
        {

            float shortestDistace = Mathf.Infinity;
            float shortestDistanceFromLeft = Mathf.Infinity;
            float shortestDistanceFromRight = Mathf.Infinity;

            Collider[] characters = Physics.OverlapSphere(targetTransform.position, maximumTargetDistance - 5);
            for (int i = 0; i < characters.Length; i++)
            {
                DP_EnemyManger character = characters[i].GetComponent<DP_EnemyManger>();
                if (character != null && !character.isDead)
                {
                    Vector3 LockOnDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(character.transform.position, targetTransform.position);
                    float targetAngle = Vector3.Angle(LockOnDirection, cameraTransform.forward);
                    Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 0.1f);
                    if (character.transform.root != targetTransform.root
                    && distanceFromTarget < maximumTargetDistance
                    && targetAngle < 50f && targetAngle > -50f)
                    {
                        if (!(availableTarget.Contains(character)))
                        {
                            availableTarget.Add(character);
                        }

                    }
                }
            }
            for (int i = 0; i < availableTarget.Count; i++)
            {

                float distance = Vector3.Distance(targetTransform.position, availableTarget[i].LockOnTransform.position);
                if (distance < shortestDistace)
                {
                    shortestDistace = distance;
                    nearestLockTransform = availableTarget[i];

                }
                if (inputHandler.lockOnFlag)
                {

                    //Vector3 RelativeEnemyPosition = currentLockOnTransform.InverseTransformPoint(availableTarget[i].transform.position);
                    //var distanceToLeft = currentLockOnTransform.position.x + availableTarget[i].transform.position.x;
                    //var distanceToRight = currentLockOnTransform.position.x - availableTarget[i].transform.position.x;
                    //float distanceToLeft = Vector3.Distance(currentLockOnTransform.position, availableTarget[i].transform.position);
                    Vector3 RelativeEnemyPosition = inputHandler.transform.InverseTransformPoint(availableTarget[i].LockOnTransform.position);
                    var distanceToLeft = Vector3.Distance(inputHandler.transform.position, availableTarget[i].LockOnTransform.position);//RelativeEnemyPosition.x;
                    var distanceToRight = Vector3.Distance(inputHandler.transform.position, availableTarget[i].LockOnTransform.position);//RelativeEnemyPosition.x;
                    if (RelativeEnemyPosition.x >= 0 && distanceToLeft < shortestDistanceFromLeft
                        && currentLockOnTransform != availableTarget[i])
                    {
                        shortestDistanceFromLeft = distanceToLeft;
                        rightLockOnTarget = availableTarget[i];
                    }
                    else if (RelativeEnemyPosition.x <= 0 && distanceToRight < shortestDistanceFromRight
                        && currentLockOnTransform != availableTarget[i])
                    {
                        shortestDistanceFromRight = distanceToRight;
                        leftLockOnTarget = availableTarget[i];
                    }
                }
            }
        }
        public void ClearLockOn()
        {
            availableTarget.Clear();
            nearestLockTransform = null;
            currentLockOnTransform = null;

        }
    }
}




