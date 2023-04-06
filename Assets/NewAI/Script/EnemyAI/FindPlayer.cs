using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FindPlayer : ActionNode
{

    public float detectionRadius = 20f;
    public float minDetectionAngle = -60f;
    public float maxDetectionAngle = 60f;
    public float stoppingDistance = 1.5f;
    public float RotationSpeed = 15f;
    public LayerMask playerMask;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.player != null)
        {
            return State.Failure;
        }
        Collider[] colliders = Physics.OverlapSphere(context.transform.position, detectionRadius, playerMask);
        for (int i = 0; i < colliders.Length; i++)
        {

            Transform player = colliders[i].transform;
            // DP_CharacterStats characterStats = colliders[i].transform.GetComponent<DP_CharacterStats>();
            if (player != null && player.tag == "Player")
            {
                Vector3 targetDirection = player.position - context.transform.position;
                float viewAbleAngle = Vector3.Angle(targetDirection, context.transform.forward);

                if (viewAbleAngle < maxDetectionAngle && viewAbleAngle > minDetectionAngle)
                {
                    blackboard.player = player;
                    context.agent.enabled = true;
                    return State.Success;
                }
            }
        }
        return State.Failure;
    }
}
