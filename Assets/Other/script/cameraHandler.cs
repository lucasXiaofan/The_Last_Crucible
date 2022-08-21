using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour
{
   public Transform targetTransfrom;
   public Transform cameraTransfrom;
   public Transform cameraPivotTransfrom;
   private Transform myTransfrom;
   private Vector3 cameraTransformPosition;
   private LayerMask ignoreLayers;

   private Vector3 cameraFollowVelocity = Vector3.zero;


   public static cameraHandler singleton;
   public float lookSpeed = 0.1f;
   public float followSpeed = 0.1f;
   public float pivotSpeed = 0.03f;
   private float targetPosition;
   private float defaultPositon;
   private float lookAngle;
   public float pivotAngle;
   public float minimumPivot = -35f;
   public float maximumPivot = 35f;

   public float cameraSphereRadius = 0.2f;
   public float cameraCollisionOffset = 0.5f;
   public float minimumCollisionOffset = 0.5f;


    private void Awake() 
    {
        singleton = this;
        myTransfrom = transform;
        defaultPositon = cameraTransfrom.localPosition.z;
        ignoreLayers= ~(1<<8|1<<9|1<<10);
    }
    public void FollowTarget(float delta) 
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTransfrom.position,
            targetTransfrom.position,ref cameraFollowVelocity,delta/followSpeed);
        //Lerp(myTransfrom.position,targetTransfrom.position,delta/followSpeed);
        myTransfrom.position = targetPosition;
        HandleCameraCollision(delta);
    }
    public void HandleCameraRotation(float delta,float mouseXinput, float mouseYInput)
    {
        lookAngle +=(mouseXinput*lookSpeed)/delta;
        pivotAngle -= (mouseYInput * pivotSpeed)/delta;
        pivotAngle = Mathf.Clamp(pivotAngle,minimumPivot,maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransfrom.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransfrom.localRotation = targetRotation;
    }

    private void HandleCameraCollision(float delta)
    {
        targetPosition = defaultPositon;//Q1 why define targetpositon to defualt position?
        RaycastHit hit;
        Vector3 direction = cameraTransfrom.position - cameraPivotTransfrom.position;
        direction.Normalize();

        if (Physics.SphereCast
        (cameraPivotTransfrom.position,cameraSphereRadius,direction, out hit, Mathf.Abs(targetPosition),ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTransfrom.position,hit.point);
            targetPosition = -(dis - cameraCollisionOffset);
        }
        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = - minimumCollisionOffset;
        }
        cameraTransformPosition.z = Mathf.Lerp(cameraTransfrom.localPosition.z,targetPosition,delta/0.2f);
        cameraTransfrom.localPosition = cameraTransformPosition;

    }



   



}
