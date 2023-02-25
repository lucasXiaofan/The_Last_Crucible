using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    public class DP_PlayerHealthBar : MonoBehaviour
    {
        public Slider playerHealthBar;
        public RectTransform postureBar;
        float POSTURE_BAR_WIDTH = 1f;
        public Image postureBarImage;
        private void Start()
        {
            if(postureBar != null)
            {
                SetPosture(0.1f);
            }
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

        public void SetPosture(float postureNormalized) {
        if (postureBar == null) return;
        postureBar.sizeDelta = new Vector2(postureNormalized * POSTURE_BAR_WIDTH, postureBar.sizeDelta.y);
        Color postureBarColor = new Color(1, 1 - postureNormalized * 1f, 0);
        postureBarImage.color = postureBarColor;
    }
    }
}
