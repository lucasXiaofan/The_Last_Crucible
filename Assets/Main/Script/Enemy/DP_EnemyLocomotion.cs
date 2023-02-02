using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyLocomotion : MonoBehaviour
    {
        DP_EnemyManger enemyManger;
        public LayerMask detectionLayer;
        private void Awake()
        {
            enemyManger = GetComponent<DP_EnemyManger>();
        }

        public void HandleDetection()
        {
            // 1.having a Collider[] that store all the detected player
            // 2. using physics.overlapSphere(position, radius, layer)
            /// 
            /// 3. for each detected object if character stats != null, 
            ///     currentCharacter = object
            //      1.calculate the targetdirection = targetposition - transform location
            ///     2.calculate the viewable angle using Vector3.Angle(with target, transform.?)
            ///     if view angle in range of maxview and minview
            ///     current target = currentCharacter
            /// 
            ///
        }
    }
}