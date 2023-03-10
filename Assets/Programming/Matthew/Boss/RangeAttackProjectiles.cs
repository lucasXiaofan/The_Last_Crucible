using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class RangeAttackProjectiles : MonoBehaviour
    {
        public DP_EnemyManger enemyManger;
        public DP_EnemyLocomotion enemyLocomotion;
        public DP_EnemyAnimator enemyAnimator;

        [Header("RangeAttack1_1")]
        public DP_BossRangeAttack1_1 rangeAttack1_1State;

        
        [Header("RangeAttack1_2")]
        public DP_BossRangeAttack1_2 rangeAttack1_2State;


        public void startMove()
        {
            rangeAttack1_1State.navMeshOldSpeed = enemyLocomotion.navMeshAgent.speed;
            enemyLocomotion.navMeshAgent.speed = rangeAttack1_1State.navMeshNewSpeed;

            enemyLocomotion.navMeshAgent.enabled = true;
            enemyLocomotion.navMeshAgent.updateRotation = false;
            rangeAttack1_1State.moveBoss(enemyLocomotion, enemyManger, enemyAnimator.anim.GetInteger("dodgeDir"));
        }

        public void determineNewDir()
        {
            int moveDir = Random.Range(0, 3);
            enemyAnimator.anim.SetInteger("dodgeDir", moveDir);
        }

        public void stopMove()
        {
            enemyLocomotion.navMeshAgent.speed = rangeAttack1_1State.navMeshOldSpeed;
            enemyLocomotion.navMeshAgent.updateRotation = true;
            enemyLocomotion.navMeshAgent.enabled = false;
        }

        public void fireProjectile1_1()
        {
            Quaternion targetDir = rangeAttack1_1State.HandleRotation(enemyLocomotion, enemyManger);

            rangeAttack1_1State.ProjectileAttack(targetDir);
        }

        //RangeAttack1_2
        public void jumpMove()
        {
            rangeAttack1_2State.startJump(enemyAnimator, enemyManger, enemyLocomotion);
        }

        public void landMove()
        {
            rangeAttack1_2State.startLand(enemyAnimator, enemyManger, enemyLocomotion);
        }

        public void fireProjectile1_2()
        {
            Quaternion targetDir = rangeAttack1_2State.HandleRotation(enemyLocomotion, enemyManger);

            rangeAttack1_2State.ProjectileAttack(targetDir);
        }
    }
}
