using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_PlayerSoundManager : MonoBehaviour
    {
        AudioSource audioSource;
        AudioSource ambienceSource;
        AudioSource battleMusic;
        
        [Header("Sword Swing Sound")]
        public AudioClip[] SwordSwingSound;
        public float swingVolume = 0.7f;
        [Header("Magic Sound")]
        public AudioClip[] magicSound;
        public float magicSoundVolume = 0.7f;
        [Header("Taking Damage Sound")]
        public AudioClip[] takeDamageSound;
        private List<AudioClip> potentialDamageSound;
        private AudioClip lastDamageSoundPlayed;
        public float hitVolume = 0.7f;
        [Header("ExecuteSound")]
        public AudioClip[] executeSound;
        public float ExecuteVolume = 1f;
        [Header("Parry Sound")]
        public AudioClip[] parrySound;
        private List<AudioClip> potentialParrySound;
        private AudioClip lastParrySoundPlayed;
        public float parryVolume = 0.85f;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void PlayRandomDamageSoundFX()
        {
            // potentialDamageSound = new List<AudioClip>();
            // foreach (var damagesound in takeDamageSound)
            // {
            //     if(damagesound != lastDamageSoundPlayed)
            //     {
            //         potentialDamageSound.Add(damagesound);
            //     }
            // }
            // int randomValue = Random.Range(0,potentialDamageSound.Count);
            // lastDamageSoundPlayed = takeDamageSound[randomValue];
            audioSource.PlayOneShot(takeDamageSound[0],hitVolume);    
        }
        public void playSwingSound(int index)
        {
            audioSource.PlayOneShot(SwordSwingSound[index],swingVolume);
        }
        public void playExecutionSound()
        {
            audioSource.PlayOneShot(executeSound[0],ExecuteVolume);    
        }
        public void FrontExecution1()
        {
            audioSource.PlayOneShot(executeSound[1],ExecuteVolume);
        }
        public void FrontExecution2()
        {
            audioSource.PlayOneShot(executeSound[2],ExecuteVolume);
        }
        public void PlayParrySound()
        {
            potentialParrySound = new List<AudioClip>();
            foreach (var damagesound in parrySound)
            {
                if(damagesound != lastParrySoundPlayed)
                {
                    potentialParrySound.Add(damagesound);
                }
            }
            int randomValue = Random.Range(0,potentialParrySound.Count);
            lastParrySoundPlayed = parrySound[randomValue];
            audioSource.PlayOneShot(parrySound[randomValue],parryVolume); 

        }
    }
}
