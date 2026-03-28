using UnityEngine;
using UnityEngine.UI;

public class PlayerProgressionUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text experienceText;
    [SerializeField] private Slider experienceSlider;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null || gameManager.ProgressionSystem == null)
            return;

        gameManager.ProgressionSystem.OnProgressChanged += ProgressChangedHandler;
        RefreshUI();
    }

    private void OnDestroy()
    {
        if (gameManager != null && gameManager.ProgressionSystem != null)
        {
            gameManager.ProgressionSystem.OnProgressChanged -= ProgressChangedHandler;
        }
    }

    private void ProgressChangedHandler(int level, int currentExperience)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        PlayerProgressionSystem progression = gameManager.ProgressionSystem;
        int currentLevel = progression.CurrentLevel;
        int currentExp = progression.CurrentExperience;
        int levelMinExp = progression.GetCurrentLevelMinExperience();
        int nextLevelExp = progression.GetNextLevelRequiredExperience();
        int expInsideCurrentLevel = Mathf.Max(0, currentExp - levelMinExp);
        int expForLevelUp = Mathf.Max(1, nextLevelExp - levelMinExp);

        if (levelText != null)
            levelText.text = $"LVL {currentLevel}";

        if (experienceText != null)
        {
            if (progression.IsMaxLevel)
                experienceText.text = "MAX";
            else
                experienceText.text = $"{expInsideCurrentLevel}/{expForLevelUp}";
        }

        if (experienceSlider != null)
        {
            experienceSlider.minValue = 0;
            experienceSlider.maxValue = expForLevelUp;
            experienceSlider.value = progression.IsMaxLevel ? expForLevelUp : expInsideCurrentLevel;
        }
    }
}
