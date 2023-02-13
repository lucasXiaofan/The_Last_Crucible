using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{

    public class DP_DropKey : MonoBehaviour
    {
        DP_EnemyManger enemyManger;
        public GameObject item;
        // Start is called before the first frame update
        void Start()
        {
            enemyManger = GetComponentInParent<DP_EnemyManger>();
            item.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            if (enemyManger.isDead)
            {

                item.SetActive(true);
            }
        }
    }
}
