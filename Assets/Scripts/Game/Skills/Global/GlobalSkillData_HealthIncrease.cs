using UnityEngine;

namespace ZombieIo.Character.Skills.GlobalSkills
{
	[CreateAssetMenu(fileName = "HealthIncrease", menuName = "Skills/Global Skills/HealthIncrease", order = 1)]
	public class GlobalSkillData_HealthIncrease : GlobalSkillData
	{
		[SerializeField] private float[] healthIncreasePercent;
		
		
		public float[] HealthIncreasePercent => healthIncreasePercent;
	}
}