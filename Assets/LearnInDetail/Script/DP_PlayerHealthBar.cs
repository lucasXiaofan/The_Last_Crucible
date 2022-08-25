using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_PlayerHealthBar : MonoBehaviour
    {
        public Slider playerHealthBar;
        private void Start()
        {
            playerHealthBar = GetComponent<Slider>();
        }
        public void SetMaximumHeath(int maximumHealth)
        {
            playerHealthBar.maxValue = maximumHealth;
            playerHealthBar.value = maximumHealth;
        }
        public void SetHeathBarValue(int currentHealth)
        {
            playerHealthBar.value = currentHealth;
        }
        public bool alive()
        {
            return playerHealthBar.value > 0;
        }
    }
}
