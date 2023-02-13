using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ArcherTooCloseState : DP_State
    {
        public DP_ArcherIdleTest idleState;
        public DP_ArcherChaseState chaseState;
        private DP_CharacterStats chaserTarget = null;
        private bool isRunning = true;
        private Quaternion? newDirection;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (isRunning) {
                // Should never happen.
                if (enemyLocomotion.currentTarget == null)
                {
                    return idleState;
                }
                // Set our tracked target
                if (chaserTarget == null)
                {
                    chaserTarget = enemyLocomotion.currentTarget;
                }
                // If we are a good distance away. EXIT STATE
                if (Vector3.Distance(transform.position, chaserTarget.transform.position) >= enemyLocomotion.stoppingDistance - 5)
                {
                    isRunning = false;
                    chaserTarget = null;
                    enemyLocomotion.currentTarget = null;
                    return this;
                }
                if () // TODO Get away from target.
            } else {
                if (!newDirection.HasValue)
                {
                    newDirection = Quaternion.Inverse(enemyManger.transform.rotation);
                }
                else {
                    enemyLocomotion.HandleDetection();
                    if (enemyLocomotion.currentTarget != null) {
                        return chaseState;
                    }
                    if (enemyManger.transform.rotation == (Quaternion)newDirection) {
                        return idleState;
                    }
                    enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, (Quaternion) newDirection, enemyLocomotion.RotationSpeed / Time.deltaTime);
                }
                
            }
            return this;
        }

    }
}
