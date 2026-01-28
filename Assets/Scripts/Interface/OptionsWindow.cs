using UnityEngine;
using UnityEngine.UI;
using ZombieIo.AudioSystem;

public class OptionsWindow : Window
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private Button closeButton;


    public override void Initialize()
    {
        musicToggle.onValueChanged.AddListener(MusicToggleHandler);
        soundsToggle.onValueChanged.AddListener(SoundsToggleHandler);
        closeButton.onClick.AddListener(CloseOptionsHandler);
    }

    private void CloseOptionsHandler()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    private void SoundsToggleHandler(bool isEnable)
    {
        SimpleAudioSystemService.Instance.SetVolume(AudioSystemType.Sounds, isEnable);
        SimpleAudioSystemService.Instance.SetVolume(AudioSystemType.UISounds, isEnable);
    }

    private void MusicToggleHandler(bool isEnable)
    {
        SimpleAudioSystemService.Instance.SetVolume(AudioSystemType.Ambient, isEnable);
    }
}
