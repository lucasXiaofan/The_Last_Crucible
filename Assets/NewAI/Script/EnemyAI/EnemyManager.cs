using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Important locations")]
    public Transform waterSource;
    public Vector3 restSpot;
    [Header("Effects")]
    public ParticleSystem fireVFX;
    [Header("Status")]
    public bool isFired;
    public bool isPanic;
    public bool isSleep;
    public bool isDead;
    private void Awake()
    {
        waterSource = FindObjectOfType<Water>().position;
        restSpot = this.transform.position;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Animation functions
    public void TurnOnFire() 
    {
        fireVFX.Play();
    }
    public void TurnOffFire() 
    {
        fireVFX.Stop();
    }
    #endregion
}
