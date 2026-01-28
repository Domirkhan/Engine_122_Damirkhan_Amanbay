using UnityEngine;

namespace ZombieIo
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ZombieIO/GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private float gameTimeMinutesMax = 15;

        [Space(10), Header("Experience progress")]
        [SerializeField]
        private int baseExperience = 20;
        [SerializeField]
        private int grownRate = 10;
        
        [Space(10), Header("SpawnLogic")]
        [SerializeField]
        private float spawnEnemyTimeSec = 2;
        [SerializeField]
        private float minEnemySpawnDistance = 5;
        [SerializeField]
        private float maxEnemySpawnDistance = 15;
        
        
        public float GameTimeMinutesMax => 
            gameTimeMinutesMax;
        
        public int BaseExperience => 
            baseExperience;
        
        public int GrownRate => 
            grownRate;
        
        public float GameTimeSecondsMax => 
            gameTimeMinutesMax * 60;
        
        public float SpawnEnemyTimeSec => 
            spawnEnemyTimeSec;
        
        public float MinEnemySpawnDistance =>
            minEnemySpawnDistance;
        
        public float MaxEnemySpawnDistance =>
            maxEnemySpawnDistance;
    }
}