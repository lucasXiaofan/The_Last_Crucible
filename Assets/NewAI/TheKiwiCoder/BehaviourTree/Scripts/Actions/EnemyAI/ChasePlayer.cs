using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ChasePlayer : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 0.1f;
    public float chaseDistance = 5f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;
    protected override void OnStart()
    {

        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.player.position;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (context.agent.pathPending)
        {
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance)
        {
            context.agent.enabled = false;
            return State.Failure;
        }
        else if (context.agent.remainingDistance > chaseDistance)
        {
            context.agent.enabled = false;
            return State.Failure;
        }

        else if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}
