using UnityEngine;
using Voodoo.Pattern;

public class UIManager : SingletonMB<UIManager>
{
	public GameObject MainCanvas;

	public GameObject MainMenuCanvas;

	//public GameObject ShopCanvas;

	public CanvasGroup AllUI;

	private void Awake()
	{
	}

	public void ShowMainUI()
	{
		if (MainMenuCanvas != null) MainMenuCanvas.SetActive(true);
	}

	public void HideMainUI()
	{
		if (MainMenuCanvas != null) MainMenuCanvas.SetActive(false);
	}

	internal void HideAllUI()
	{
		if (AllUI != null) AllUI.alpha = 0f;
	}
}
