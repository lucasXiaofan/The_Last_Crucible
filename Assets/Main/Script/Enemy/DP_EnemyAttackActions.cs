using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Actions")]

    public class DP_EnemyAttackActions : DP_EnemyActions
    {
        public int score = 3;
        public float recoveryTime = 2f;
        public float maxAttackAngle = 35f;
        public float minAttackAngle = -35f;
        public float minDistanceNeededToAttack = 0;
        public float maxDistanceNeededToAttack = 3f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
