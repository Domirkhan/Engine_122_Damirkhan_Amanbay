using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZombieIo.Character.Skills.GlobalSkills;

public class UpgradeCharacterWindow : Window
{
	[SerializeField]
	private GlobalCharacterUpgradePopupController prefab;
	[SerializeField]
	private RectTransform parent;
	[SerializeField]
	private Button closeWindow;
	
	private List<GlobalCharacterUpgradePopupController> upgradePopupControllers;
	
	
	public override void Initialize()
	{
		upgradePopupControllers = new();

		CreateGlobalCharacterUpgradePopupController(GlobalSkillType.HealthIncrease);
		CreateGlobalCharacterUpgradePopupController(GlobalSkillType.ItemPickUpDistanceIncrease);
		
		closeWindow.onClick.AddListener(
			() =>
			{
				Hide(false);
				GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
			});
	}
	
	private void CreateGlobalCharacterUpgradePopupController(GlobalSkillType skillType)
	{
		var container = GameObject.Instantiate<GlobalCharacterUpgradePopupController>(prefab, parent);
		container.Initialize(skillType);
		upgradePopupControllers.Add(container);
	}
}
