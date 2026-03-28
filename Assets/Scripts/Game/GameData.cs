using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private int sessionTimeMinutes = 15;
    [SerializeField] private float timeBetweenEnemySpawn = 1.5f;
    [SerializeField] private float minSpawnOffset = 7f;
    [SerializeField] private float maxSpawnOffset = 18f;

    public int SessionTimeMinutes => sessionTimeMinutes;
    public int SessionTimeSeconds => sessionTimeMinutes * 60;
    public float TimeBetweenEnemySpawn => timeBetweenEnemySpawn;
    public float MinSpawnOffset => minSpawnOffset;
    public float MaxSpawnOffset => maxSpawnOffset;
}
