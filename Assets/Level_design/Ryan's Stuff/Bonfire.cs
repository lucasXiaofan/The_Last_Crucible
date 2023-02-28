using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP;

public class Bonfire : MonoBehaviour
{
    public SpawnManager spawnManager;
    public ParticleSystem fire;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager.spawnPosition == this.transform.position)
        {
            fire.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Saved at: " + this.transform.position);
            fire.gameObject.SetActive(true);
            spawnManager.spawnPosition = this.transform.position;

            
        }
    }
}
