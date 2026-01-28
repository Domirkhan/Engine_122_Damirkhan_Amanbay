namespace ZombieIo.Character.Skills
{
	public interface ISkillService
	{
		T GetGlobalSkillByType<T>() where T : GlobalSkills.GlobalSkillData;
		
		GlobalSkills.GlobalSkillData GetGlobalSkillByEnum(GlobalSkills.GlobalSkillType type);

		int GetGlobalSkillLevel(GlobalSkills.GlobalSkillType type);

		void SetGlobalSkillLevel(GlobalSkills.GlobalSkillType type, int level);
	}
}