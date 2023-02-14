using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DP
{
    public class DP_HealthBarLookAtPlayer : MonoBehaviour
    {
        
        DP_CameraControl cameraControl;
        void Start()
        {
            
            cameraControl = FindObjectOfType<DP_CameraControl>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction = cameraControl.cameraTransform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,30f/Time.deltaTime);
            
        }
    }
}