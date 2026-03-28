using UnityEngine;

/// <summary>
/// Фоновая музыка и короткие SFX. Повесь на тот же объект, что и GameManager, или отдельный объект в сцене.
/// </summary>
public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance { get; private set; }

    [Header("Источники (можно два AudioSource на одном GO)")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Клипы (подложи свои .wav / .mp3 в проект)")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip shootClip;

    [SerializeField] [Range(0f, 1f)] private float musicVolume = 0.45f;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 0.7f;

    [Tooltip("Запускать ли музыку при загрузке сцены")]
    [SerializeField] private bool playMusicOnAwake = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (musicSource != null)
            musicSource.volume = musicVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;

        if (playMusicOnAwake)
            StartBackgroundMusic();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void StartBackgroundMusic()
    {
        if (musicSource == null || backgroundMusic == null)
            return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlayShoot()
    {
        if (sfxSource == null || shootClip == null)
            return;

        sfxSource.PlayOneShot(shootClip, sfxVolume);
    }
}
