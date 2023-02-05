using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyManger : DP_Character
    {
        DP_EnemyLocomotion enemyLocomotion;
        public bool isPreformingAction;

        [Header("A.I Setting")]
        public float detectionRadius = 20f;
        public float minDetectionAngle = -60f;
        public float maxDetectionAngle = 60f;

        private void Awake()
        {
            enemyLocomotion = GetComponent<DP_EnemyLocomotion>();
        }
        private void Update()
        {

        }

        private void FixedUpdate()
        {
            HandleCurrentAction();
        }
        private void HandleCurrentAction()
        {
            if (enemyLocomotion.currentTarget == null)
            {
                print(enemyLocomotion.currentTarget);
                enemyLocomotion.HandleDetection();
            }
            else
            {
                enemyLocomotion.HandleMovement();
            }


        }
    }
}
