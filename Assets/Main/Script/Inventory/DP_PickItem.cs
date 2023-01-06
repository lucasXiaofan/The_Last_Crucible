using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PickItem : MonoBehaviour
    {
        public float radius = 0.6f;
        public string ItemName;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }
        public virtual void Interact(DP_PlayerManager playerManager)
        {
            //print("you succesfully interacted");
        }
    }
}
