using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_EnemyStats : MonoBehaviour
    {
        public DP_PlayerHealthBar playerHealthBar;
        Animator anim;
        public int maximumHealth;
        public int currentHealth;
        public int healthLevel = 10;
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            maximumHealth = SetMaximumHealthLevel();
            currentHealth = maximumHealth;
            playerHealthBar.SetMaximumHeath(maximumHealth);
        }
        private int SetMaximumHealthLevel()
        {
            maximumHealth = healthLevel * 10;
            return maximumHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            playerHealthBar.SetHeathBarValue(currentHealth);
            anim.Play("getHit");

            if (currentHealth <= 0)
            {
                anim.Play("dead");
            }
        }

    }
}

