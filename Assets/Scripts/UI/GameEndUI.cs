using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Панель победы/поражения. Назначь в инспекторе панель, тексты и кнопки.
/// Лучше вешать этот скрипт на активный объект (например Canvas), а не на выключенный EndGamePanel —
/// иначе Unity не вызовет OnEnable и подписка не создастся; для этого есть BindAll из GameManager.
/// </summary>
public class GameEndUI : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text titleText;
    [SerializeField] private Text statsText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button backToMenuButton;

    [Tooltip("Опционально: скрывать кнопку Start на время игры и показывать после конца")]
    [SerializeField] private GameObject startGameButtonRoot;

    /// <summary>Находит все GameEndUI (даже на выключенных объектах) и подписывает на GameManager.</summary>
    public static void BindAllToGameManager()
    {
        if (GameManager.Instance == null)
            return;

        var list = Resources.FindObjectsOfTypeAll<GameEndUI>();
        foreach (var ui in list)
        {
            if (ui == null)
                continue;
            if (!ui.gameObject.scene.IsValid())
                continue;
            ui.SubscribeToGameManager();
        }
    }

    private void Awake()
    {
        if (endPanel != null)
            endPanel.SetActive(false);
    }

    private void OnEnable()
    {
        SubscribeToGameManager();
    }

    /// <summary>Идемпотентно: сначала отписываемся, потом подписываемся.</summary>
    private void SubscribeToGameManager()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnGameEnded -= OnGameEnded;
        GameManager.Instance.OnGameEnded += OnGameEnded;
        GameManager.Instance.OnSessionStarted -= OnSessionStarted;
        GameManager.Instance.OnSessionStarted += OnSessionStarted;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameEnded -= OnGameEnded;
            GameManager.Instance.OnSessionStarted -= OnSessionStarted;
        }
    }

    private void OnSessionStarted()
    {
        if (startGameButtonRoot != null)
            startGameButtonRoot.SetActive(false);
    }

    private void Start()
    {
        SubscribeToGameManager();

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        if (backToMenuButton != null)
            backToMenuButton.onClick.AddListener(OnBackToMenuClicked);
    }

    private void OnDestroy()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.RemoveListener(OnPlayAgainClicked);
        if (backToMenuButton != null)
            backToMenuButton.onClick.RemoveListener(OnBackToMenuClicked);
    }

    private void OnGameEnded(GameEndSummary summary)
    {
        if (endPanel != null)
            endPanel.SetActive(true);

        if (titleText != null)
            titleText.text = summary.Reason == GameEndReason.Victory ? "Победа!" : "Игра окончена";

        if (statsText != null)
        {
            statsText.text =
                $"Счёт: {summary.SessionScore}\n" +
                $"Рекорд: {summary.BestScore}\n" +
                $"Уровень: {summary.Level}";
        }

        if (startGameButtonRoot != null)
            startGameButtonRoot.SetActive(true);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnPlayAgainClicked()
    {
        if (endPanel != null)
            endPanel.SetActive(false);
        if (GameManager.Instance != null)
            GameManager.Instance.RestartAndPlay();
    }

    private void OnBackToMenuClicked()
    {
        if (endPanel != null)
            endPanel.SetActive(false);
        if (GameManager.Instance != null)
            GameManager.Instance.ReturnToStartScreen();
    }
}
