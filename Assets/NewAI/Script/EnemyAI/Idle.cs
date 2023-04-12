using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Idle : ActionNode
{
    public float walkSpeed = 5f;
    public float toleranceFromInitalPoint = 0.5f;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        float distanceFromInitial = Vector3.Distance(context.transform.position, context.manager.restSpot);
        if (distanceFromInitial < toleranceFromInitalPoint)
        {
            Resting();
        }
        // if (context.animator.GetBool("isInteracting") == true)
        // {
        //     return State.Running;
        // }
        else
        {
            WalkBack();
        }
        return State.Success;
    }
    private void WalkBack()
    {
        context.agent.enabled = true;
        context.agent.SetDestination(context.manager.restSpot);
        context.agent.speed = walkSpeed;
        context.animator.SetFloat("Vertical", 0.5f);
    }
    private void Resting()
    {
        context.agent.enabled = false;
        context.playAnimation("Happy", false);
        context.animator.SetFloat("Vertical", 0f);
    }
}
