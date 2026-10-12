using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Oil : MonoBehaviour
{
	[Header("Custom Settings")]
	public float fuelConsumptionRate = 15f;
	public bool EcoMode;

	//public DOTweenAnimation EcoParent;

	public Image EcoBar;

	public Image EcoVisual;

	private int _currentLevel;

	[SerializeField]
	private RectTransform _sliderImage;

	[SerializeField]
	private Image _fillImage;

	[SerializeField]
	private Image _bonusFillImage;

	[SerializeField]
	private GameObject _lowOil;

	[SerializeField]
	private RectTransform _gaugeParent;

	public Vector2 Smallest;

	public Vector2 Biggest;

	private float CurrentOilValue;

	private float CurrentBonusOilValue;

	public MoveDirection Joystick;

	private BubbleBonusManager _bubbleBonusManager;

	private float step;

	public Item MaxOilUpgrade;

	private bool _spendingOil;

	public bool IsSpendingOil => _spendingOil;

	private float _spendTimer;

	public Transform _oilImage;

	private int CurrentOilLevel;

	private bool _isMaxed = false;
	private bool _initialized = false;
	private int lastUpgradeLevel = -1;

	public float MaxOil { get; private set; } = 100f;

	public float OilValue => CurrentOilValue;

	private void Start()
	{
		UpdateValues();
	}

			public void UpdateValues()
	{
		int oilUpgrade = PlayerPrefs.GetInt("OilUpgrade", 0);
		MaxOil = 50f + oilUpgrade * 25f;
		
		_isMaxed = MaxOilUpgrade != null && oilUpgrade >= MaxOilUpgrade.Max;

		if (!_initialized)
		{
			_initialized = true;
			lastUpgradeLevel = oilUpgrade;

			float savedOil = PlayerPrefs.GetFloat("SavedOilValue", -1f);
			int currentLvl = PlayerPrefs.GetInt("Level", 1);
			int hasPlayed = PlayerPrefs.GetInt("HasPlayedLvl1", 0);

			if (currentLvl <= 1 && hasPlayed == 0)
			{
				CurrentOilValue = MaxOil * 0.50f;
				PlayerPrefs.SetInt("HasPlayedLvl1", 1);
			}
			else if (savedOil >= 0f)
			{
				CurrentOilValue = savedOil;
			}
			else
			{
				CurrentOilValue = MaxOil * 0.50f;
			}
		}
		else
		{
			if (oilUpgrade > lastUpgradeLevel)
			{
				int diff = oilUpgrade - lastUpgradeLevel;
				CurrentOilValue += MaxOil * 0.10f * diff;
				lastUpgradeLevel = oilUpgrade;
			}
		}

		if (_isMaxed)
		{
			CurrentOilValue = MaxOil;
		}

		CurrentOilValue = Mathf.Clamp(CurrentOilValue, 0f, MaxOil);
		// Saved in OnDisable

		if (_fillImage != null && MaxOil > 0f)
		{
			_fillImage.fillAmount = CurrentOilValue / MaxOil;
		}
		if (_lowOil != null)
		{
			_lowOil.SetActive((CurrentOilValue / MaxOil) < 0.2f && !_isMaxed);
		}
	}

	public void StopSpend()
	{
		_spendingOil = false;
	}

		public void StartSpendingOil()
	{
		UpdateValues();
		if (CurrentOilValue <= 0f) {
			CurrentOilValue = MaxOil * 0.1f; // minimal fuel to prevent instant loop
		}
		_spendingOil = true;
	}

	private IEnumerator SpendOil()
	{
		yield return null;
	}

	public void AddOil(int value)
	{
		CurrentOilValue = Mathf.Clamp(CurrentOilValue + value, 0f, MaxOil);
		if (_fillImage != null)
		{
			_fillImage.fillAmount = CurrentOilValue / MaxOil;
		}
		if (CurrentOilValue <= 0f)
		{
			CurrentOilValue = 0f;
			_spendingOil = false;
			Debug.Log("[GamePlayTest] Oil depleted to 0! Calling GameManager.EndGame().");
			if (GameManager.Instance != null && !GameManager.Instance.finised)
			{
				GameManager.Instance.EndGame();
			}
		}
	}

	public void AnimateOil()
	{
	}

	private void OnDisable()
	{
		PlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);
	}

			private void Update()
	{
		if (!_spendingOil) return;

		if (Joystick == null)
		{
			Joystick = UnityEngine.Object.FindAnyObjectByType<DynamicJoystick>();
			if (Joystick == null) Joystick = UnityEngine.Object.FindAnyObjectByType<Joystick>();
		}

		bool isMoving = false;
		if (Joystick != null && Joystick.Direction.sqrMagnitude > 0.01f)
		{
			isMoving = true;
		}
		else if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
		{
			isMoving = true;
		}

		if (isMoving)
		{
			if (!_isMaxed)
			{
				CurrentOilValue -= Time.deltaTime * fuelConsumptionRate;
			}
			// Saved in OnDisable
			if (CurrentOilValue <= 0f)
			{
				CurrentOilValue = 0f;
				PlayerPrefs.SetFloat("SavedOilValue", 0f);
				_spendingOil = false;
				if (_fillImage != null) _fillImage.fillAmount = 0f;
				if (GameManager.Instance != null && !GameManager.Instance.finised)
				{
					GameManager.Instance.EndGame();
				}
				return;
			}

			if (_fillImage != null && MaxOil > 0f)
			{
				_fillImage.fillAmount = CurrentOilValue / MaxOil;
			}

			if (_lowOil != null)
			{
				_lowOil.SetActive(CurrentOilValue / MaxOil < 0.2f);
			}
		}
	}
}
