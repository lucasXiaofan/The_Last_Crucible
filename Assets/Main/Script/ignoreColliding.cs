using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreColliding : MonoBehaviour
{
    public CapsuleCollider characterCollider;
    public CapsuleCollider blocker;
    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, blocker, true);
    }
}
