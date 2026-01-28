using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ZombieIo.AudioSystem
{
    public interface IAudioSystemService
    {
        Dictionary<AmbientType, AudioClip> AmbientsDictionary { get; }
        
        Dictionary<SoundType, AudioClip> SoundsDictionary { get; }

        Dictionary<UiSoundType, AudioClip> UiSoundsDictionary { get; }


        void Initialize();

        void SetVolume(AudioSystemType type, float volume);

        void SetVolume(AudioSystemType type, bool status);

		AudioMixerGroup GetAudioMixerGroup(AudioSystemType type);

        public AudioSource CreateAudioSource(GameObject audioSourceParent, AudioSystemType type);
    }
}
