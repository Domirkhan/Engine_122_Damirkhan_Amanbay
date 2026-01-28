using System;
using UnityEngine;

public class ScoreManager
{
    private const string SESSION_SCORE_MAX = "save_score_max";
    private const string CURRENT_SCORE = "save_current_score";


    public event Action<int> OnScoreUpdated;
    public event Action<int> OnSessionScoreUpdated;
    public event Action<int> OnScoreChanged;
    
    
    private int gameScore;
    private int globalGameScore;
    private int scoreMax;

    public int GameScore => gameScore;
    public int GlobalGameScore
    {
        get => globalGameScore;
        set
        {
            globalGameScore = value;
            if (globalGameScore < 0)
                globalGameScore = 0;
            
            PlayerPrefs.SetInt(CURRENT_SCORE, globalGameScore);
        }
    }
    public int ScoreMax => scoreMax;
    public bool IsNewScoreRecord { get; private set; }


    public ScoreManager()
    {
        gameScore = 0;
        scoreMax = PlayerPrefs.GetInt(SESSION_SCORE_MAX, 0);
        globalGameScore = PlayerPrefs.GetInt(CURRENT_SCORE, 0);
        IsNewScoreRecord = false;
    }

    public void StartGame()
    {
        gameScore = 0;
        IsNewScoreRecord = false;
    }
    
    public void CharacterDeathHandler(Character character)
    {
        gameScore += character.CharacterData.ScoreCost;
        OnScoreChanged?.Invoke(gameScore);
        
        if (gameScore <= scoreMax)
            return;

        scoreMax = GameScore;
        PlayerPrefs.SetInt(SESSION_SCORE_MAX, scoreMax);
        IsNewScoreRecord = true;
    }
    
    public void CompleteMatch()
    {
        GlobalGameScore += gameScore;
    }
}
