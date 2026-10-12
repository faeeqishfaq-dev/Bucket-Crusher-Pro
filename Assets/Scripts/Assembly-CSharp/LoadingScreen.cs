using UnityEngine;
using Voodoo.Pattern;

public class LoadingScreen : SingletonMB<LoadingScreen>
{
	public GameObject panel;

	private void Awake()
	{
		Hide();
	}

	public void Show()
	{
		if (panel != null) panel.SetActive(true);
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		if (panel != null) panel.SetActive(false);
		gameObject.SetActive(false);
	}
}
