using UnityEngine;

namespace ZombieIo
{
	public class ExperienceFormula
	{
		private readonly int baseExperience;
		private readonly int grownRate;


		public ExperienceFormula(int baseExperience, int grownRate)
		{
			this.baseExperience = baseExperience;
			this.grownRate = grownRate;
		}


		public int GetExperienceByLevel(int level)
		{
			return (int)(baseExperience + grownRate * Mathf.Pow(1.5f, (level - 1)));
		}
	}
}