using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZombieIo.Character.Skills
{
    public class SkillService : MonoBehaviour, ISkillService
    {
        [SerializeField] private GlobalSkills.GlobalSkillsCollection globalSkillsCollection;


        public T GetGlobalSkillByType<T>() where T : GlobalSkills.GlobalSkillData
        {
            return globalSkillsCollection.GetGlobalSkill<T>();
        }
        
        public GlobalSkills.GlobalSkillData GetGlobalSkillByEnum(GlobalSkills.GlobalSkillType type)
        {
            return globalSkillsCollection.GetGlobalSkill(type);
        }

        public int GetGlobalSkillLevel(GlobalSkills.GlobalSkillType type)
        {
            return PlayerPrefs.GetInt("GlobalSkillLevel_" + type, 0);
        }
        
        public void SetGlobalSkillLevel(GlobalSkills.GlobalSkillType type, int level)
        {
            PlayerPrefs.SetInt("GlobalSkillLevel_" + type, level);
        }
    }
}