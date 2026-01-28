using UnityEngine;


namespace ZombieIo.Character.Skills.GlobalSkills
{
	[CreateAssetMenu(fileName = "ItemPickUpDistanceIncrease", menuName = "Skills/Global Skills/ItemPickUpDistanceIncrease", order = 1)]
	public class GlobalSkillData_ItemPickUpDistanceIncrease : GlobalSkillData
	{
		[SerializeField] private float[] itemPickUpDistanceIncreasePercent;
		
		
		public float[] ItemPickUpDistanceIncreasePercent => itemPickUpDistanceIncreasePercent;
	}
}