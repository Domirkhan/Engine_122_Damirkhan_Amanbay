using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgressionData", menuName = "Game/Player Progression Data")]
public class PlayerProgressionData : ScriptableObject
{
    [SerializeField] private List<int> requiredExperiencePerLevel = new List<int> { 0, 10, 25, 45, 70, 100 };

    public int MaxLevel => Mathf.Max(1, requiredExperiencePerLevel.Count);

    public int GetRequiredExperienceForLevel(int level)
    {
        if (requiredExperiencePerLevel.Count == 0)
            return 0;

        int levelIndex = Mathf.Clamp(level - 1, 0, requiredExperiencePerLevel.Count - 1);
        return requiredExperiencePerLevel[levelIndex];
    }

    public int GetRequiredExperienceForNextLevel(int currentLevel)
    {
        if (currentLevel >= MaxLevel)
            return GetRequiredExperienceForLevel(MaxLevel);

        return GetRequiredExperienceForLevel(currentLevel + 1);
    }
}
