using System;
using UnityEngine;


namespace ZombieIo.AudioSystem
{
    [Serializable]
    public class SoundAudioData : AudioData
    {
        [SerializeField] private SoundType soundType;


        public SoundType SoundType => soundType;
    }
}