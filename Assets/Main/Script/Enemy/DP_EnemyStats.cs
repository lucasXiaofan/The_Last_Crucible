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
        public GameObject bloodVFX;




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
            maximumPosture = PostureLevel * 10;
            playerHealthBar.SetMaximumHeath(maximumHealth);
            playerHealthBar.hideEnemyUI();
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }
        public void handleDeath()
        {
            playerHealthBar.hideEnemyUI();
            playerHealthBar.enabled = false;
        }

        public void ShowUI()
        {
            playerHealthBar.showEnemyUI();
        }

        public void TakeDamage(int damage, bool normal)
        {

            if (enemyManger.isDead)
            {
                playerHealthBar.hideEnemyUI();
                return;
            }
            if (!normal)
            {
                playBloodVFX(enemyManger.LockOnTransform.position, normal);
            }
            playerHealthBar.showEnemyUI();
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            if (normal)
            {
                enemyManger.soundManager.PlayRandomDamageSoundFX();
                if (PostureBreak())
                {
                    DamagePosture(damage / 3);
                }
                else
                {
                    DamagePosture(damage / 3);
                    if(!enemyManger.Boss)
                    {
                        enemyAnimator.ApplyTargetAnimation("getHit", true, false);
                    }
                    


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
        public void playBloodVFX(Vector3 location, bool normal)
        {
            
            if (normal)
            {
                GameObject blood = Instantiate(bloodVFX, location, Quaternion.identity);
            }
            // else
            // {
            //     GameObject blood = Instantiate(CricialBlood, location, Quaternion.identity);
            // }

        }

        public void DamagePosture(int damage)
        {
            playerHealthBar.showEnemyUI();
            recoveryTimer = 0;
            currentPosture += damage;
            currentPosture = (currentPosture >= maximumPosture ? maximumPosture : currentPosture);
            playerHealthBar.SetPosture(currentPosture / maximumPosture);
            if (PostureBreak())
            {
                enemyAnimator.ApplyTargetAnimation("vulnerable", true, false);

            }

        }



        public bool PostureBreak()
        {
            return currentPosture >= maximumPosture;
        }

        public void RecoverPosture()
        {
            recoveryTimer += Time.deltaTime * 3;

            if (recoveryTimer >= recoveryLimit)
            {
                if (currentPosture <= maximumPosture && currentPosture > 0)
                {
                    currentPosture -= Time.deltaTime * 10;
                }
                if (currentPosture <= 0)
                {
                    currentPosture = 1;
                }
            }
            playerHealthBar.SetPosture(currentPosture / maximumPosture);
        }
    }
}

