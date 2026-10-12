using UnityEngine.EventSystems;
using Voodoo.Pattern;

public class ShopButtonGroup : SingletonMB<ShopButtonGroup>
{
	public SkinButton SkinButton;

	public UpgradesButton UpgradesButton;

	public CityBuilderButton CityBuilderButton;

	private EventSystem eventSystem;

	private void Start()
	{
	}

	private void GameStarted()
	{
	}

	private void SettingsShow()
	{
	}

	public void HideUpgrades()
	{
	}

	public void HideSkins()
	{
	}

	private void AnimStarted()
	{
	}

	private void AnimCompleted()
	{
	}

	private void Update()
	{
	}
}
