using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    public float lifetime = 3f;
    void Awake()
    {
        Destroy(gameObject,lifetime);
    }
}
