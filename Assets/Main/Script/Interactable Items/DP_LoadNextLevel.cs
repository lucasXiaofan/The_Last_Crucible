using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_LoadNextLevel : MonoBehaviour
    {
        SpawnManager spawnManager;
        private void Start()
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DP_PlayerManager playerManager = other.GetComponent<DP_PlayerManager>();
                if (playerManager != null)
                {

                    spawnManager.spawnPosition.Set(5.39860153f, -18.8751335f, 162.049423f);
                    playerManager.LoadNextScene();
                }
            }
        }
    }
}
