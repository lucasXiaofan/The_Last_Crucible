using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class RangeAttackProjectiles : MonoBehaviour
    {
        public DP_EnemyManger enemyManger;
        public DP_EnemyLocomotion enemyLocomotion;

        [Header("RangeAttack1_1")]
        public DP_BossRangeAttack1_1 rangeAttack1_1State;
        
        public void startMove()
        {
            rangeAttack1_1State.navMeshOldSpeed = enemyLocomotion.navMeshAgent.speed;
            enemyLocomotion.navMeshAgent.speed = rangeAttack1_1State.navMeshNewSpeed;

            enemyLocomotion.navMeshAgent.enabled = true;
            rangeAttack1_1State.moveBoss(enemyLocomotion);
        }

        public void stopMove()
        {
            enemyLocomotion.navMeshAgent.speed = rangeAttack1_1State.navMeshOldSpeed;
            enemyLocomotion.navMeshAgent.enabled = false;
        }

        public void fireProjectile1_1()
        {
            Quaternion targetDir = rangeAttack1_1State.HandleRotation(enemyLocomotion, enemyManger);

            rangeAttack1_1State.ProjectileAttack(targetDir);
        }

        /*
        [Header("Range Attack 1_2")]
        public DP_BossRangeAttack1_2 rangeAttack1_2State;
        
        private GameObject projectileObj;
        public GameObject rangeAttack1_2Prefab;

        public void fireProjectile1_2()
        {
            Quaternion targetDir = rangeAttack1_2State.HandleRotation(enemyLocomotion, enemyManger);

            rangeAttack1_2State.ProjectileAttack(targetDir);
        }

        public void jumpMotion1_2()
        {
            rangeAttack1_2State.jumpMotion(enemyManger, 0.83f);
        }

        public void landMotion1_2()
        {
            rangeAttack1_2State.landMotion(enemyManger, 0.83f);
        }
        */
    }
}
