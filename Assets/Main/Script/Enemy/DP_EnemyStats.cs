using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStats : DP_CharacterStats
    {
        public DP_PlayerHealthBar playerHealthBar;
        DP_EnemyAnimator enemyAnimator;
        DP_EnemyManger enemyManger;



        [Header("Posture")]
        public float recoveryLimit = 10f;
        float recoveryTimer = 0;


        void Start()
        {
            enemyAnimator = GetComponentInChildren<DP_EnemyAnimator>();
            enemyManger = GetComponent<DP_EnemyManger>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            currentPosture = 1;
            maximumPosture = PostureLevel *10 ;
            playerHealthBar.SetMaximumHeath(maximumHealth);
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }


        public void TakeDamage(int damage, bool normal)
        {
            recoveryTimer = 0;
            if (enemyManger.isDead)
            {
                return;
            }
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            if (normal)
            {
                if(PostureBreak())
                {
                    DamagePosture(damage);
                }
                else
                {
                    DamagePosture(damage);
                    enemyAnimator.ApplyTargetAnimation("getHit", true, false);

                }
                if (currentHealth <= 0)
                {
                    enemyManger.isDead = true;
                    enemyAnimator.ApplyTargetAnimation("dead", true, false);
                }
            }
            if (currentHealth <= 0)
            {
                enemyManger.isDead = true;
            }

        }

        public void DamagePosture(int damage)
        {
            
            currentPosture+=damage;
            currentPosture= (currentPosture >= maximumPosture? maximumPosture:currentPosture);
            playerHealthBar.SetPosture(currentPosture/maximumPosture);
            if( PostureBreak())
            {
                enemyAnimator.ApplyTargetAnimation("vulnerable",true,false);
            }
        }

        public bool PostureBreak()
        {
            return currentPosture >= maximumPosture;
        }

        public void RecoverPosture()
        {
            recoveryTimer-=Time.deltaTime;
            
            if( recoveryTimer >= recoveryLimit)
            {
                if( currentPosture < maximumPosture && currentPosture >0)
                {
                    currentPosture-=Time.deltaTime*10;
                }
                if(currentPosture<=0)
                {
                    currentPosture = 1;
                }
            }
            playerHealthBar.SetPosture(currentPosture/maximumPosture);
        }
    }
}

