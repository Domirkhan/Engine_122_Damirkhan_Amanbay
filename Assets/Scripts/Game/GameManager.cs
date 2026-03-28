using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private PlayerProgressionData playerProgressionData;

    private ScoreSystem scoreSystem;
    private PlayerProgressionSystem progressionSystem;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private bool isGameActive;


    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => characterFactory;
    public PlayerProgressionSystem ProgressionSystem => progressionSystem;

    /// <summary>Вызывается после Initialize игрока — подписка UI на HP.</summary>
    public event System.Action<ILiveComponent> OnPlayerLiveReady;

    public event System.Action<GameEndSummary> OnGameEnded;
    public event System.Action OnSessionStarted;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialized();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameEndUI.BindAllToGameManager();
    }

    private void Initialized()
    {
        scoreSystem = new ScoreSystem();

        if (playerProgressionData == null)
        {
            Debug.LogWarning("PlayerProgressionData is not assigned on GameManager; using a temporary default asset.");
            playerProgressionData = ScriptableObject.CreateInstance<PlayerProgressionData>();
        }

        progressionSystem = new PlayerProgressionSystem(playerProgressionData);
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive)
            return;

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
        OnPlayerLiveReady?.Invoke(player.LiveComponent);

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        scoreSystem.StartGame();
        progressionSystem.StartNewSession();

        // ????? ????????? UI, ????? ?????? ?? ??????????? ?????? Start
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

        isGameActive = true;
        OnSessionStarted?.Invoke();
    }

    /// <summary>Очистить всех активных персонажей (после конца игры или перед новым стартом).</summary>
    public void ClearSessionCharacters()
    {
        if (characterFactory == null)
            return;

        var snapshot = new List<Character>(characterFactory.ActiveCharacters);
        foreach (Character c in snapshot)
        {
            if (c == null)
                continue;
            if (c.LiveComponent != null)
                c.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
            c.gameObject.SetActive(false);
            characterFactory.ReturnCharacter(c);
        }
    }

    public void ReturnToStartScreen()
    {
        ClearSessionCharacters();
    }

    public void RestartAndPlay()
    {
        ClearSessionCharacters();
        StartGame();
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        if (timeBetweenEnemySpawn < 0)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.Data.ScoreCost);
                progressionSystem.AddExperience(deathCharacter.Data.ScoreCost);
                break;
        }
        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);

        deathCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(
            playerPosition.x + GetOffset(),
            0,
            playerPosition.z + GetOffset()
        );
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;



        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return ((isPlus) ? offset : (-1 * offset));
        }
    }

    private void GameVictory()
    {
        scoreSystem.EndGame();
        isGameActive = false;
        RaiseGameEnded(GameEndReason.Victory);
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        isGameActive = false;
        RaiseGameEnded(GameEndReason.GameOver);
    }

    private void RaiseGameEnded(GameEndReason reason)
    {
        var summary = new GameEndSummary(
            reason,
            scoreSystem.Score,
            scoreSystem.MaxScore,
            progressionSystem.CurrentLevel);
        OnGameEnded?.Invoke(summary);
    }
}