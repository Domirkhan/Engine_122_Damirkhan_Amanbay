using System;

public class PlayerProgressionSystem
{
    private readonly PlayerProgressionData progressionData;

    public int CurrentLevel { get; private set; }
    public int CurrentExperience { get; private set; }
    public bool IsMaxLevel => CurrentLevel >= progressionData.MaxLevel;

    public event Action<int, int> OnProgressChanged;

    public PlayerProgressionSystem(PlayerProgressionData progressionData)
    {
        this.progressionData = progressionData;
        StartNewSession();
    }

    public void StartNewSession()
    {
        CurrentLevel = 1;
        CurrentExperience = 0;
        NotifyProgressChanged();
    }

    public void AddExperience(int experience)
    {
        if (experience <= 0 || IsMaxLevel)
            return;

        CurrentExperience += experience;

        while (!IsMaxLevel && CurrentExperience >= progressionData.GetRequiredExperienceForNextLevel(CurrentLevel))
        {
            CurrentLevel++;
        }

        if (IsMaxLevel)
        {
            CurrentExperience = progressionData.GetRequiredExperienceForLevel(CurrentLevel);
        }

        NotifyProgressChanged();
    }

    public int GetCurrentLevelMinExperience()
    {
        return progressionData.GetRequiredExperienceForLevel(CurrentLevel);
    }

    public int GetNextLevelRequiredExperience()
    {
        return progressionData.GetRequiredExperienceForNextLevel(CurrentLevel);
    }

    private void NotifyProgressChanged()
    {
        OnProgressChanged?.Invoke(CurrentLevel, CurrentExperience);
    }
}
