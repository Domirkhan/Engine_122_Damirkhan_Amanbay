using UnityEngine;

namespace ZombieIo.Character.Skills.GlobalSkills
{
	public abstract class GlobalSkillData : ScriptableObject
	{
		[SerializeField] private string nameKey;
		[SerializeField] private string descriptionKey;
		[SerializeField] private Sprite icon;
		[SerializeField] private int[] skillCosts;
		
		
		public string NameKey => nameKey;
		public string DescriptionKey => descriptionKey;
		public Sprite Icon => icon;
		public int[] SkillCosts => skillCosts;
	}
}