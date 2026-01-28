using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZombieIo.Character.Skills;
using ZombieIo.Character.Skills.GlobalSkills;

public class GlobalCharacterUpgradePopupController : MonoBehaviour
{
	[SerializeField] private TMP_Text skillName;
	[SerializeField] private TMP_Text skillDescription;
	[SerializeField] private TMP_Text upgradeParameter;
	[SerializeField] private TMP_Text costText;
	[SerializeField] private Button buyButton;
	
	private GlobalSkillType type;
	private GlobalSkillData skillData;
	private int skillLevel;

	
	private SkillService SkillService =>
		GameManager.Instance.SkillService;
	

	public void Initialize(GlobalSkillType type)
	{
		this.type = type;
		this.skillLevel = SkillService.GetGlobalSkillLevel(type);
		skillData = SkillService.GetGlobalSkillByEnum(type);
		this.skillName.text = skillData.NameKey;
		this.skillDescription.text = skillData.DescriptionKey;
		costText.text = skillData.SkillCosts[skillLevel].ToString();

		buyButton.onClick.AddListener(OnClickBuyButton);
	}
	
	public void OnClickBuyButton()
	{
		if (GameManager.Instance.ScoreManager.GlobalGameScore < skillData.SkillCosts[skillLevel])
			return;

		GameManager.Instance.ScoreManager.GlobalGameScore -= skillData.SkillCosts[skillLevel];
		skillLevel++;
		
		SkillService.SetGlobalSkillLevel(type, skillLevel);
		costText.text = skillData.SkillCosts[skillLevel].ToString();
	}
}
