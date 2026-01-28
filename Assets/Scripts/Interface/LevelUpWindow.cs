using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpWindow : Window
{
	[Space][SerializeField]
	private Button continueButton;
	[SerializeField]
	private TMP_Text levelText;
	
	
	public override void Initialize()
	{
		continueButton.onClick.AddListener(ContinueButtonClickHandler);
		GameManager.Instance.SessionExperienceManager.OnLevelUp += UpdateLevelHandler;
	}
	
	private void UpdateLevelHandler(int level)
	{
		GameManager.Instance.IsGameActive = false;
		GameManager.Instance.WindowsService.HideWindow<GameplayWindow>(true);
		levelText.text = level.ToString();
		Show(false);
	}
	
	private void ContinueButtonClickHandler()
	{
		Hide(true);
		GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
		GameManager.Instance.IsGameActive = true;
	}
	
	private void OnDestroy()
	{
		GameManager.Instance.SessionExperienceManager.OnLevelUp -= UpdateLevelHandler;
	}
}
