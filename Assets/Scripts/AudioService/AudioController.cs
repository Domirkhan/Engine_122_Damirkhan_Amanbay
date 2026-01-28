using System.Collections.Generic;
using UnityEngine;

namespace ZombieIo.AudioSystem
{
	public class AudioController
	{
		private readonly Dictionary<AudioClip, AudioSource> _loopedAudioSources;
		private readonly AudioSource _audioSource = default;
		

		public AudioController(AudioSource audioSource)
		{
			_audioSource = audioSource;
			_loopedAudioSources = new Dictionary<AudioClip, AudioSource>();
		}


		public void PlaySound(AudioClip clip, bool loop = false)
		{
			if (_audioSource == null || clip == null)
				return;

			_audioSource.clip = clip;
			_audioSource.loop = loop;
			_audioSource.Play();
		}

		public void PlayOneShotSound(AudioClip clip)
		{
			if (_audioSource == null || clip == null)
				return;

			_audioSource.PlayOneShot(clip);
		}

		public void PlayLoopedSound(AudioClip clip)
		{
			if (_audioSource == null || clip == null)
				return;

			if (_loopedAudioSources.ContainsKey(clip))
			{
				Debug.LogError($"Attempt to start play looped sound {clip.name} when it already played");
				return;
			}

			AudioSource newLoopedAudioSource = _audioSource.gameObject.AddComponent<AudioSource>();

			newLoopedAudioSource.outputAudioMixerGroup = _audioSource.outputAudioMixerGroup;
			newLoopedAudioSource.clip = clip;
			newLoopedAudioSource.loop = true;
			newLoopedAudioSource.Play();

			_loopedAudioSources.Add(clip, newLoopedAudioSource);
		}

		public void StopLoopedSound(AudioClip clip)
		{
			if (_audioSource == null || clip == null)
				return;

			if (!_loopedAudioSources.TryGetValue(clip, out AudioSource loopedAudioSource))
				return;

			loopedAudioSource.Stop();
			Object.Destroy(loopedAudioSource);
			_loopedAudioSources.Remove(clip);
		}

		public void StopSound()
		{
			if (_audioSource == null)
				return;

			_audioSource.Stop();
		}
	}
}
