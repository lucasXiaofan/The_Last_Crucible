using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_LockOnTransform : MonoBehaviour
{
    public Transform followTarget;
    public int followSpeed = 5;
    void FixedUpdate()
    {
        float time = Time.deltaTime;
        FollowTarget(time);
    }
    public void FollowTarget(float delta)
    {
        Vector3 CameraVelocity = Vector3.zero;
        Vector3 targetP = Vector3.SmoothDamp(transform.position, followTarget.position, ref CameraVelocity, delta / 0.1f);
        transform.position = targetP;
    }
}
