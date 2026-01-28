using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Slider healthSlider;

    [Space][SerializeField]
    private Slider experienceSlider;

    [Space] [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private TMP_Text scoreText;
    

    protected override void OpenStart()
    {
        base.OpenStart();
        var player = GameManager.Instance.CharacterFactory.PlayerCharacter;

        UpdateHealthVisual(player);
        player.HealthComponent.OnCharacterHealthChange += UpdateHealthVisual;

        ScoreChangeHandler(GameManager.Instance.ScoreManager.GameScore);
        GameManager.Instance.ScoreManager.OnScoreChanged += ScoreChangeHandler;

        UpdateExperience(GameManager.Instance.SessionExperienceManager.Experience,
            GameManager.Instance.SessionExperienceManager.ExperienceMax);

        GameManager.Instance.SessionExperienceManager.OnExperienceUp += UpdateExperience;
        
        UpdateTimer();
    }

    protected override void CloseStart()
    {
        base.CloseStart();
        
        GameManager.Instance.ScoreManager.OnScoreChanged -= ScoreChangeHandler;
        GameManager.Instance.SessionExperienceManager.OnExperienceUp -= UpdateExperience;
        
        var player = GameManager.Instance.CharacterFactory.PlayerCharacter;
        if (player == null)
            return;
        
        player.HealthComponent.OnCharacterHealthChange -= UpdateHealthVisual;
    }

    private void UpdateHealthVisual(Character character)
    {
        int health = (int)character.HealthComponent.Health;
        int healthMax = character.HealthComponent.HealthMax;
        
        healthText.text = health + "/" + healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
    }
    
    private void ScoreChangeHandler(int score)
    {
        scoreText.text = score.ToString();
    }
    
    private void UpdateTimer()
    {
        var min = (int)(GameManager.Instance.GameTime / 60);
        var sec = (int)(GameManager.Instance.GameTime % 60);
        timerText.text = GetTime(min) + ":" + GetTime(sec);


        string GetTime(int value)
        {
            return (value < 10) ? "0" + value : value.ToString();
        }
    }
    
    private void UpdateExperience(int value, int maxValue)
    {
        experienceSlider.maxValue = maxValue;
        experienceSlider.value = value;
    }
    
    private void Update()
    {
        UpdateTimer();
    }
}
