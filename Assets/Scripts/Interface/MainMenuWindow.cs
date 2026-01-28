using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button optionsGameButton;


    public override void Initialize()
    {
        startGameButton.onClick.AddListener(StartGameHandler);
        upgradeButton.onClick.AddListener(UpgradeHandler);
        optionsGameButton.onClick.AddListener(OpenOptionsHandler);
    }

    protected override void OpenEnd()
    {
        base.OpenEnd();
        startGameButton.interactable = true;
        optionsGameButton.interactable = true;
    }

    protected override void CloseStart()
    {
        base.CloseStart();
        startGameButton.interactable = false;
        optionsGameButton.interactable = false;
    }

    private void StartGameHandler()
    {
        GameManager.Instance.GameStart();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
        Hide(false);
    }

    private void UpgradeHandler()
    {
        GameManager.Instance.WindowsService.ShowWindow<UpgradeCharacterWindow>(false);
        Hide(false);
    }

    private void OpenOptionsHandler()
    {
        Hide(false);
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(false);
    }
}
