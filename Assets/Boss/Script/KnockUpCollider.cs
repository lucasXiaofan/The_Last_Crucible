using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class KnockUpCollider : MonoBehaviour
    {
        CapsuleCollider KpCollider;
        public float KnockUpDamage;
        private void Start()
        {
            KpCollider = GetComponent<CapsuleCollider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //player animation apply damage
            }
        }
    }
}
