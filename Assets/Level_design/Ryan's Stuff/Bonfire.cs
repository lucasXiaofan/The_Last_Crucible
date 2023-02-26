using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP;

public class Bonfire : MonoBehaviour
{
    public DP_PlayerManager player;

    private void Start()
    {
        player = FindObjectOfType<DP_PlayerManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Saved at: " + this.transform.position);
            player.spawnManager.spawnPosition = this.transform.position;

            //other.GetComponent<DP_PlayerManager>().spawnManager.spawnPosition = this.transform.position;
            
        }
    }
}
