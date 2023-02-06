using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP{
    public abstract class DP_State : MonoBehaviour
    {
        public abstract DP_State Tick(DP_EnemyManger enemyManger, DP_EnemyStats enemyStats, DP_EnemyAnimator enemyAnimator);
    }
}
