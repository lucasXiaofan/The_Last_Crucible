using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ArcherChaseState : DP_State
    {
        public DP_ArcherIdleTest idleState;
        public DP_ArcherTooCloseState tooCloseState;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.currentTarget == null) return idleState;
            // Too Close
            if (enemyLocomotion.distanceFromtarget <= enemyLocomotion.stoppingDistance / 3) // 5 Units away 
            {
                return tooCloseState;
            }
            // Within Firing Range
            if (withinRange(enemyLocomotion.currentTarget.transform.position, enemyLocomotion.stoppingDistance, enemyManger.minDetectionAngle, enemyManger.maxDetectionAngle))
            {
                // Place holder aim code.
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                enemyLocomotion.navMeshAgent.enabled = false;
                #region Rotation
                Vector3 direction = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
                // direction.y = 0;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetRotation, enemyLocomotion.RotationSpeed / Time.deltaTime);
                #endregion
                return this;
            }
            enemyLocomotion.HandleMovement();
            return this;
        }

        private bool withinRange(Vector3 location, float range, float minAngle, float maxAngle) {
            // Check distance.
            if (Vector3.Distance(transform.position, location) > range) return false;
            Vector3 targetDirection = location - transform.position;
            // Check viewable angle.
            float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
            if (viewAbleAngle > maxAngle || viewAbleAngle < minAngle) return false;
            /* Check raycast. Removed because of possible looping issue. -> Player holding corner, AI tries to chase, gets too close, but still doesn't have LOS, chases again, ...
             * Uncomment if you wish to test.
            Vector3 toTarget = location - transform.position;
            RaycastHit hit;
            Physics.Raycast(transform.position, toTarget, out hit);
            if (!hit.transform.CompareTag("Player")) return false;
            */
            return true;
        }

    }
}
