using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class EventManager : MonoBehaviour
    {
        //Door to be locked when 
        [SerializeField]
        private GameObject door;

        [SerializeField]
        private DP_EnemyStats miniBoss;
        [SerializeField]
        private GameObject miniBossUI;

        [SerializeField]
        private DP_EnemyStats enemy;

        [SerializeField]
        private Animator doorAnimation;

        private bool played = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            //if boss ui is active and enemey is alive then close door (rotation)
            if (other.gameObject.tag == "Player")
            {
                if(played == false)
                {
                    if (miniBoss.currentHealth >= 1 && enemy.currentHealth >= 1)
                    {
                        //play door close animation 
                        doorAnimation.Play("Door Close");
                        played = true;
                    }
                }

            }
            //if miniboss health <= 0 then open the door 
            if (miniBoss.currentHealth <= 0 && played == true)
            {
                //opening door animation
                doorAnimation.Play("Door open animation");
            }
        }

    }

    
}