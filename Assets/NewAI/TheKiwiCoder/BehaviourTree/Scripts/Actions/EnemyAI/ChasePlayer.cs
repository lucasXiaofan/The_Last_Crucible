using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ChasePlayer : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 1f;
    public float chaseDistance = 10f;
    public bool updateRotation = true;
    public float rotationSpeed = 10f;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;
    protected override void OnStart()
    {
        context.agent.enabled = true;
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
            // context.agent.enabled = false;
            // context.animator.SetFloat("Vertical", 0.5f, 0.2f, Time.deltaTime);
            return State.Failure;
        }
        else if (context.agent.remainingDistance > chaseDistance)
        {
            // context.agent.enabled = false;
            // context.animator.SetFloat("Vertical", 0f, 0.2f, Time.deltaTime);
            return State.Failure;
        }

        else if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        Vector3 direction = blackboard.player.position - context.transform.position;
        direction.y = 0;
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
        context.animator.SetFloat("Vertical", 1f, 0.1f, Time.deltaTime);

        return State.Running;
    }

}
