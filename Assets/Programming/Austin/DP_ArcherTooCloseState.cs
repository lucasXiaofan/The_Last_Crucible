using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_ArcherTooCloseState : DP_State
    {
        public DP_ArcherIdleTest idleState;
        public DP_ArcherChaseState chaseState;
        private bool isRunning = true; // If we are running away or turning around.
        private Vector3 newDirection = Vector3.zero; // Last known direction of player.
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {
            if (isRunning)
            {
                // Should never happen.
                if (enemyLocomotion.currentTarget == null)
                {
                    return idleState;
                }
                // If we are a good distance away. EXIT STATE
                if (Vector3.Distance(transform.position, enemyLocomotion.currentTarget.transform.position) >= (2 * enemyLocomotion.stoppingDistance) / 3) // Greater than or equal to 2/3 the stopping distance.
                {
                    enemyLocomotion.navMeshAgent.enabled = false;
                    isRunning = false;
                    newDirection = enemyLocomotion.currentTarget.transform.position - enemyLocomotion.transform.position;
                    enemyLocomotion.currentTarget = null;
                }
                // Otherwise, move to a position 2/3 the stopping distance away from the player.
                else
                {
                    enemyAnimator.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                    enemyAnimator.anim.SetFloat("Archer", 0, 0.1f, Time.deltaTime);
                    enemyLocomotion.navMeshAgent.enabled = true;
                    Vector3 fleeDirection = (enemyLocomotion.currentTarget.transform.position - this.transform.position).normalized;
                    enemyLocomotion.navMeshAgent.SetDestination(this.transform.position - (fleeDirection * (2 * enemyLocomotion.stoppingDistance) / 3)); // Move 2/3 the stopping distance away.
                }
            }
            else
            {
                // Begin transitioning to still state.
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                enemyLocomotion.HandleDetection();

                // If we found a target while turning around, switch to chase. Still trying to make smoother.
                if (enemyLocomotion.currentTarget != null && enemyAnimator.anim.GetFloat("Vertical") < 0.1f)
                {
                    enemyAnimator.anim.SetFloat("Vertical", 0);
                    isRunning = true;
                    return chaseState;
                }
                // If we've turned around fully, switch to idle.
                if (enemyLocomotion.transform.rotation == Quaternion.LookRotation(newDirection) && enemyAnimator.anim.GetFloat("Vertical") < 0.1f)
                {
                    enemyAnimator.anim.SetFloat("Vertical", 0);
                    enemyAnimator.anim.SetFloat("Archer", 0);
                    isRunning = true;
                    return idleState;
                }
                enemyLocomotion.transform.rotation = Quaternion.RotateTowards(enemyLocomotion.transform.rotation, Quaternion.LookRotation(newDirection), enemyLocomotion.RotationSpeed * 5 * Time.deltaTime);
            }
            return this;
        }

    }
}
