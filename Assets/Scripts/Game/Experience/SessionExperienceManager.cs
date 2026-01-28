using System;

namespace ZombieIo
{
    public class SessionExperienceManager
    {
        public event Action<int> OnLevelUp;
        public event Action<int, int> OnExperienceUp;
        
        
        private ExperienceFormula experienceFormula;
        private int currentExperience;

        public int Experience
        {
            get => currentExperience;
            set
            {
                currentExperience = value;
                if (currentExperience >= ExperienceMax)
                {
                    currentExperience -= ExperienceMax;
                    Level++;
                    ExperienceMax = experienceFormula.GetExperienceByLevel(Level);
                    OnLevelUp?.Invoke(Level);
                }
                
                OnExperienceUp?.Invoke(currentExperience, ExperienceMax);
            }
        }
        
        public int ExperienceMax { get; private set; }
        
        public int Level { get; private set; }


        public SessionExperienceManager(GameData gameData)
        {
            experienceFormula = new ExperienceFormula(gameData.BaseExperience, gameData.GrownRate);
        }


        public void DropProgress()
        {
            Experience = 0;
            Level = 1;
            ExperienceMax = experienceFormula.GetExperienceByLevel(Level);
        }
    }
}