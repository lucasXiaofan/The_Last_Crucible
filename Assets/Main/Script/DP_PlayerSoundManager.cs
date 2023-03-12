using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerSoundManager : MonoBehaviour
    {
        AudioSource audioSource;
        [Header("Taking Damage Sound")]
        public AudioClip[] takeDamageSound;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void PlayDamageSoundFX()
        {

        }
    }
}
