using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_HealthBarLookAtPlayer : MonoBehaviour
    {
        Transform cameraPOV;
        void Start()
        {
            cameraPOV = FindObjectOfType<DP_CameraControl>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position - cameraPOV.transform.position);
        }
    }
}