using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyAttackState : DP_State
    {
        public DP_EnemyPursueState pursueState;

        public override DP_State Tick(DP_EnemyManger enemyManger,
                                    DP_EnemyStats enemyStats,
                                    DP_EnemyAnimator enemyAnimator,
                                    DP_EnemyLocomotion enemyLocomotion)
        {
            if (enemyLocomotion.distanceFromtarget > enemyLocomotion.stoppingDistance)
            {

                return pursueState;
            }
            else if (enemyManger.Recovering)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0.5f);
            }
            // Hanle rotation
            enemyLocomotion.navMeshAgent.enabled = false;
            Vector3 AttackDirection = enemyLocomotion.currentTarget.transform.position - enemyManger.transform.position;
            Quaternion targetDir = Quaternion.LookRotation(AttackDirection);
            enemyManger.transform.rotation = Quaternion.Slerp(enemyManger.transform.rotation, targetDir, Time.deltaTime / 0.1f);
            enemyManger.AttackTarget(0);


            // enemyLocomotion.transform.position += enemyAnimator.anim.deltaPosition;
            // Vector3 jump = enemyLocomotion.transform.position;
            // jump.y += enemyAnimator.rootPosition.position.y;
            // enemyLocomotion.transform.position = jump;
            // print("how much enemy jumped: " + enemyAnimator.rootPosition.position.y + "how much jumped? " + enemyLocomotion.transform.position.y);

            return this;
        }
    }
}
