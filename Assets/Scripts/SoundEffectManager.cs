using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class SoundEffectManager : MonoBehaviour
    {
        public AudioClip Dig;
        public AudioClip Cut;
        public AudioClip Water;
        public AudioClip Blend;
        public AudioClip Plant;

        public AudioSource AS;
        
        public void PlayAudio(AudioClip clip)
        {
            AS.PlayOneShot(clip);
        }
    }
}