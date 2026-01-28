using System;
using System.Collections.Generic;
using UnityEngine;


namespace ZombieIo.Character.Skills.GlobalSkills
{
	[CreateAssetMenu(fileName = "GlobalSkillsCollection", menuName = "Skills/Global Skills/GlobalSkillsCollection", order = 1)]
	public class GlobalSkillsCollection : ScriptableObject
	{
		[SerializeField] private GlobalSkillInfo[] globalSkills;
		private Dictionary<GlobalSkillType, GlobalSkillData> globalSkillsDictionary;


		public GlobalSkillData GetGlobalSkill(GlobalSkillType type)
		{
			if (globalSkillsDictionary == null)
			{
				Initialize();
			}

			if (!globalSkillsDictionary.ContainsKey(type))
			{
				Debug.LogError($"Unknown Skill {type}");
				return null;
			}

			return globalSkillsDictionary[type];
		}

		public T GetGlobalSkill<T>() where T : GlobalSkillData
		{
			foreach (var globalSkill in globalSkills)
			{
				if (globalSkill is T)
					return globalSkill as T;
			}
			
			Debug.LogError($"Unknown Skill {typeof(T)}");
			return null;
		}
		
		private void Initialize()
		{
			globalSkillsDictionary = new Dictionary<GlobalSkillType, GlobalSkillData>();
			foreach (var globalSkill in globalSkills)
				globalSkillsDictionary.Add(globalSkill.SkillType, globalSkill.SkillData);
		}
		
		
		[Serializable]
		public class GlobalSkillInfo
		{
			[SerializeField] private GlobalSkillType skillType;
			[SerializeField] private GlobalSkillData skillData;
			
			
			public GlobalSkillType SkillType => skillType;
			public GlobalSkillData SkillData => skillData;
		}
	}
}