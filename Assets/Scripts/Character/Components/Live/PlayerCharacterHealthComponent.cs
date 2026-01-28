using ZombieIo.Character.Skills;
using ZombieIo.Character.Skills.GlobalSkills;

public class PlayerCharacterHealthComponent : CharacterHealthComponent
{
	private int addedHealthByGlobalSkills;
	private int addedHealthBySessionSkills;
	
	
	public override int HealthMax =>
		baseHealthMax + addedHealthByGlobalSkills + addedHealthBySessionSkills;
	
	private SkillService SkillService => GameManager.Instance.SkillService;
	
	
	public override void Initialize(Character character)
	{
		base.Initialize(character);
		
		GlobalSkillData_HealthIncrease healthIncrease = SkillService.GetGlobalSkillByType<GlobalSkillData_HealthIncrease>();
		int skillLevel = SkillService.GetGlobalSkillLevel(GlobalSkillType.HealthIncrease);
		addedHealthByGlobalSkills = (int)(healthIncrease.HealthIncreasePercent[skillLevel] * baseHealthMax);

		currentHealth = HealthMax;
	}
}
