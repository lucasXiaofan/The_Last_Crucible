using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Flee : ActionNode
{
    private float fleeSpeed = 10f;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (!context.manager.isFired)
        {
            return State.Failure;
        }
        context.agent.enabled = true;
        context.agent.speed = fleeSpeed;
        context.agent.SetDestination(context.manager.waterSource.position);
        context.animator.SetFloat("Vertical", 1.5f);
        float distanceFromWater = Vector3.Distance(context.manager.waterSource.position, context.transform.position);
        if (distanceFromWater < 0.1f)
        {
            context.manager.isFired = false;
            return State.Success;
        }

        return State.Running;
    }
}
