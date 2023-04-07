using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform waterSource;
    public bool isFired;
    public bool isPanic;
    public bool isSleep;
    public bool isDead;
    private void Awake()
    {
        waterSource = FindObjectOfType<Water>().position;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
