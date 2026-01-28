using System;
using UnityEngine;


namespace ZombieIo.AudioSystem
{
    [Serializable]
    public class UiSoundAudioData : AudioData
    {
        [SerializeField] private UiSoundType _uiSoundType;
        
        
        public UiSoundType UiSoundType => _uiSoundType;
    }
}