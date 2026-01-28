using UnityEngine;
using ZombieIo;
using ZombieIo.AudioSystem;
using ZombieIo.Character.Skills;
using ZombieIo.EffectsSystem;
using ZombieIo.Input;
using ZombieIo.Items;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private WindowsService windowsService;
    [SerializeField] private AudioSystemService audioSystemService;
    [SerializeField] private EffectsFactory effectsFactory;
    [SerializeField] private ItemsService itemsService;
    [SerializeField] private SkillService skillService;

    [Space, SerializeField]
    private GameData _gameData;
    
    private bool _isGameActive = false;
    private float _gameTimeSec = 0;
    private float _spawnEnemyTimeSec = 0;

    
    public static GameManager Instance { get; private set; }

    public CharacterFactory CharacterFactory => 
        characterFactory;
    
    public WindowsService WindowsService => 
        windowsService;
    
    public AudioSystemService AudioSystemService => 
        audioSystemService;
    
    public EffectsFactory EffectsFactory => 
        effectsFactory;

    public ItemsService ItemsService =>
        itemsService;
    
    public ScoreManager ScoreManager { get; private set; }
    
    public SessionExperienceManager SessionExperienceManager { get; private set; }
    
    public IInputService InputService { get; private set; }
    public SkillService SkillService => skillService;

    public float GameTime =>
        _gameTimeSec;

    public bool IsGameActive
    {
        get => _isGameActive;
        set => _isGameActive = value;
    }


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameStart()
    {
        var player = CharacterFactory.CreateCharacter(CharacterType.DefaultPlayer);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;
        player.gameObject.SetActive(true);

        SessionExperienceManager.DropProgress();
        ScoreManager.StartGame();
        
        _gameTimeSec = 0;
        _isGameActive = true;
    }

    private void CharacterDeathHandler(Character character)
    {
        Debug.LogError("character " + character.gameObject.name + " is dead");
        switch (character.CharacterType)
        {
            case CharacterType.DefaultPlayer:
                Debug.LogError("GameOver!");
                Debug.LogError("Score = " + ScoreManager.GameScore);
                Debug.LogError("ScoreMax = " + ScoreManager.ScoreMax);
                ScoreManager.CompleteMatch();
                _isGameActive = false;
                break;
            
            case CharacterType.DefaultEnemy:
                var item = ItemsService.GetItem(ItemsService.ItemClass.SmallExperience, character.transform.position);
                item.SetDistanceForPick(5f);
                
                ScoreManager.CharacterDeathHandler(character);
                Debug.LogError("Score = " + ScoreManager.GameScore);
                break;
        }
        
        CharacterFactory.ReturnToPool(character);
        character.gameObject.SetActive(false);
        
        character.HealthComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void SpawnEnemy()
    {
        var character = CharacterFactory.CreateCharacter(CharacterType.DefaultEnemy);

        float posX = CharacterFactory.PlayerCharacter.transform.position.x + GetRandomCoordOffset();
        float posZ = CharacterFactory.PlayerCharacter.transform.position.z + GetRandomCoordOffset();
        Vector3 spawnPoint = new Vector3(posX, 0, posZ);
        character.transform.position = spawnPoint;
        character.Initialize();
        character.HealthComponent.OnCharacterDeath += CharacterDeathHandler;
        character.gameObject.SetActive(true);

        
        float GetRandomCoordOffset()
        {
            bool isPlus = Random.Range(0, 1) > 0;
            float randomOffset = Random.Range(_gameData.MinEnemySpawnDistance, _gameData.MaxEnemySpawnDistance);
            return isPlus
                ? randomOffset
                : -randomOffset;
        }
    }

    private void Initialize()
    {
        ScoreManager = new ScoreManager();
        InputService = new NewInputService();
        SessionExperienceManager = new SessionExperienceManager(_gameData);
        windowsService.Initialize();
    }

    private void Update()
    {
        if (!_isGameActive)
            return;

        _gameTimeSec += Time.deltaTime;
        _spawnEnemyTimeSec += Time.deltaTime;
        
        if (_spawnEnemyTimeSec >= _gameData.SpawnEnemyTimeSec)
        {
            SpawnEnemy();
            _spawnEnemyTimeSec = 0;
        }
        
        if (_gameTimeSec >= _gameData.GameTimeSecondsMax)
        {
            Debug.Log("Game Over! Time's up!");
            ScoreManager.CompleteMatch();
            _isGameActive = false;
        }
    }
}
