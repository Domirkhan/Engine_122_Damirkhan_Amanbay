using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieIo.AudioSystem
{
    [Serializable]
    public abstract class AudioData
    {
        [SerializeField] private AudioClip audioClip;


        public AudioClip AudioClip => audioClip;
    }
}