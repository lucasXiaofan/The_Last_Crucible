using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DP
{
    public class Boss_CombatStanceState : DP_State
    {
        public DP_State[] RangeAttackStates;
        public string[] RecoverMoves;
        public DP_State PursueState;
        public DP_State StandOffState;

        public float walkSpeed = 3.2f;

        //after boss attacked player always return to this state to select next attack;
        public override DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator, DP_EnemyLocomotion enemyLocomotion)
        {

            if (enemyManger.Recovering && !enemyManger.isPreformingAction)
            {
                // return this;

                int move = Random.Range(0, 10);
                if (move < 1)
                {
                    enemyAnimator.ApplyTargetAnimation(RecoverMoves[move], true, false);
                }
                else
                {
                    return StandOffState;
                }
            }
            else if (!enemyManger.Recovering && !enemyManger.isPreformingAction)
            {
                enemyLocomotion.navMeshAgent.enabled = false;
                int doRangeAttack = Random.Range(0, 3);
                // print("do range attacks? " + doRangeAttack);
                if (doRangeAttack == 0)
                {
                    int attack = Random.Range(0, RangeAttackStates.Length);
                    return RangeAttackStates[attack];
                }
                return PursueState;
            }
            enemyLocomotion.transform.position += enemyAnimator.anim.deltaPosition;
            return this;
            // return StandOffState;

        }
    }
}
