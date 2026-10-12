using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenView : MonoBehaviour
{
	public CanvasGroup _group;

	private Action RVAction;

	private Action NoThanksAction;

	public List<GameObject> _NoOilObjects;

	public List<GameObject> _LevelCompleteObjects;

	public List<GameObject> _LevelFailedObjects;

	public TextMeshProUGUI _earnedText;

	public TextMeshProUGUI _multiplyText;

	private EndScreenMode _currentMode;

	private bool _showing;

	private void Awake()
	{
		if (!_showing)
		{
			Hide();
		}
	}

	private void Start()
	{
		if (MessageCenter.Instance != null)
		{
			MessageCenter.Instance.ShowEndScreen += ShowScreen;
		}
	}

	private void OnDestroy()
	{
		if (MessageCenter.Instance != null)
		{
			MessageCenter.Instance.ShowEndScreen -= ShowScreen;
		}
	}

	public void ShowScreen(EndScreenMode mode, Action _rvAction, Action _noThanks)
	{
		_showing = true;
		_currentMode = mode;
		RVAction = _rvAction;
		NoThanksAction = _noThanks;
		gameObject.SetActive(true);
		if (_group != null)
		{
			_group.alpha = 1f;
			_group.interactable = true;
			_group.blocksRaycasts = true;
		}

		if (_NoOilObjects != null)
		{
			foreach (var go in _NoOilObjects) if (go != null) go.SetActive(mode == EndScreenMode.NoOIl);
		}
		if (_LevelCompleteObjects != null)
		{
			foreach (var go in _LevelCompleteObjects) if (go != null) go.SetActive(mode == EndScreenMode.LevelCompelete);
		}
		if (_LevelFailedObjects != null)
		{
			foreach (var go in _LevelFailedObjects) if (go != null) go.SetActive(mode == EndScreenMode.LevelFailed);
		}

		UnityEngine.UI.Button[] buttons = GetComponentsInChildren<UnityEngine.UI.Button>(true);
		foreach (var btn in buttons)
		{
			string btnName = btn.gameObject.name.ToLower();
			if (btnName.Contains("rv") || btnName.Contains("ad") || btnName.Contains("reward"))
			{
				btn.gameObject.SetActive(false);
			}
			if (btnName.Contains("no") && btnName.Contains("thank") || btnName.Contains("close") || btnName.Contains("skip"))
			{
				var textPro = btn.GetComponentInChildren<TextMeshProUGUI>();
				if (textPro != null) textPro.text = "NEXT";
				var textNormal = btn.GetComponentInChildren<UnityEngine.UI.Text>();
				if (textNormal != null) textNormal.text = "NEXT";
			}
		}
	}

	public void OnRVClick()
	{
		RVAction?.Invoke();
		Hide();
	}

	public void OnNoThanksClick()
	{
		Hide();
		if (NoThanksAction != null)
		{
			NoThanksAction.Invoke();
		}
		else if (GameManager.Instance != null)
		{
			GameManager.Instance.BackToMenu();
		}
	}

	private void Hide()
	{
		_showing = false;
		if (_group != null)
		{
			_group.alpha = 0f;
			_group.interactable = false;
			_group.blocksRaycasts = false;
		}
		gameObject.SetActive(false);
	}

	private void Update()
	{
	}
}
