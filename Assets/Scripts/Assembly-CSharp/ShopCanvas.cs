using UnityEngine;
using Voodoo.Pattern;

public class ShopCanvas : SingletonMB<ShopCanvas>
{
	public CanvasGroup Group;

	private bool _showing;

	private void Awake()
	{
		if (!_showing)
		{
			Hide();
		}
	}

	public void Hide()
	{
		_showing = false;
		if (Group != null)
		{
			Group.alpha = 0f;
			Group.interactable = false;
			Group.blocksRaycasts = false;
		}
		gameObject.SetActive(false);
	}

	public void Show()
	{
		_showing = true;
		gameObject.SetActive(true);
		if (Group != null)
		{
			Group.alpha = 1f;
			Group.interactable = true;
			Group.blocksRaycasts = true;
		}
	}
}
