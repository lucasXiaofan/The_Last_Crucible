using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_LoadNextLevel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DP_PlayerManager playerManager = other.GetComponent<DP_PlayerManager>();
                if (playerManager != null)
                {
                    playerManager.LoadNextScene();
                }
            }
        }
    }
}
