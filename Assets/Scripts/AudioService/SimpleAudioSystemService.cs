using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace ZombieIo.AudioSystem
{
    public class SimpleAudioSystemService : AudioSystemService
    {
		private const float DB_MINIMUM = -80f;
		private const float DB_MAXIMUM = 0f;


		[SerializeField] private AudioMixer _audioMixer;
		[SerializeField] private AudioLibrary _audioLibrary;
        
        private Dictionary<AudioSystemType, AudioMixerGroup> _mixerGroups;


        public override Dictionary<AmbientType, AudioClip> AmbientsDictionary { get; protected set; }
        public override Dictionary<SoundType, AudioClip> SoundsDictionary { get; protected set; }
        public override Dictionary<UiSoundType, AudioClip> UiSoundsDictionary { get; protected set; }
        

        public override void Initialize()
        {
            AmbientsDictionary = _audioLibrary.AmbientLibrary.GetAmbientDictionary();
            SoundsDictionary = _audioLibrary.SoundsLibrary.GetSoundsDictionary();
            UiSoundsDictionary = _audioLibrary.UiSoundsLibrary.GetUiSoundsDictionary();
            _mixerGroups = _audioLibrary.AudioMixerGroupDictionary;

            base.Initialize();
        }

        public override AudioSource CreateAudioSource(GameObject audioSourceParent, AudioSystemType type)
        {
            AudioSource audioSource = audioSourceParent.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = _mixerGroups[type];

            return audioSource;
        }

        public override void SetVolume(AudioSystemType type, float volume)
        {
            if (!isInitialized)
                return;

            var group = _mixerGroups[type];
            
            if (!group.audioMixer.SetFloat(type.ToString(),
                    Mathf.Lerp(DB_MINIMUM, DB_MAXIMUM, volume)))
            {
                Debug.LogError($"Not found {type.ToString()} audio type!");
            }
        }

        public override void SetVolume(AudioSystemType type, bool status)
        {
            SetVolume(type, status ? 1 : 0);
        }

        public override AudioMixerGroup GetAudioMixerGroup(AudioSystemType type)
        {
            return _audioMixer.FindMatchingGroups(type.ToString())[0];
        }
    }
}
