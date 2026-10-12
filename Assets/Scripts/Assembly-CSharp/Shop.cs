using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using Voodoo.Pattern;

public class Shop : SingletonMB<Shop>
{
	[SerializeField]
	private Button longButton;

	[SerializeField]
	private Button oilButton;

	[SerializeField]
	private Button powerButton;

	[SerializeField]
	private Button sizeButton;

	[SerializeField]
	private Text _longCost;

	[SerializeField]
	private Text _oilCost;

	[SerializeField]
	private Text _powerCost;

	[SerializeField]
	private Text _sizeCost;

	[SerializeField]
	private Button RvlongButton;

	[SerializeField]
	private Button RvoilButton;

	[SerializeField]
	private Button RvpowerButton;

	[SerializeField]
	private Button RvsizeButton;

	[SerializeField]
	private Text _RvlongCost;

	[SerializeField]
	private Text _RvoilCost;

	[SerializeField]
	private Text _RvpowerCost;

	[SerializeField]
	private Text _RvsizeCost;

	[SerializeField]
	private Button ForceRvPowerButton;

	[SerializeField]
	private Button ForceRvSizeButton;

	[SerializeField]
	private bool[] isForcedRvPower;

	[SerializeField]
	private bool[] isForcedRvSize;

	[SerializeField]
	private GameObject IncrementalLong;

	[SerializeField]
	private GameObject IncrementalOil;

	[SerializeField]
	private GameObject IncrementalPower;

	[SerializeField]
	private GameObject IncrementalSize;

	[SerializeField]
	private GameObject IncrementalLongOnboardingArea;

	[SerializeField]
	private GameObject IncrementalOilOnboardingArea;

	[SerializeField]
	private GameObject IncrementalPowerOnboardingArea;

	[SerializeField]
	private GameObject IncrementalSizeOnboardingArea;

	[SerializeField]
	private Text OilBonus;

	[SerializeField]
	private Text PowerBonus;

	[SerializeField]
	private Text SizeBonus;

	private Money _moneyClass;

	private Upgrade _upgrade;

	public int[] longCosts = new int[] { 10, 500, 1000, 4000, 10000 };

	public int[] oilCosts = new int[] { 5, 10, 15, 20, 30, 40, 50, 60, 70, 80, 90, 100, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 250, 260, 270, 280, 290 };

	public int[] powerCosts = new int[] { 5, 20, 100, 200, 300, 1000, 1200, 1300, 1400, 1500, 1600, 1700, 2000, 3000, 4000 };

	public int[] sizeCosts = new int[] { 15, 90, 130, 250, 350, 500, 600, 780, 935, 1024 };

	public int[] originalOilCosts;

	private int _currentLong;

	private int _currentOil;

	private int _currentPower;

	private int _currentSize;

	private int _currentLongCost;

	private int _currentOilCost;

	private int _currentPowerCost;

	private int _currentSizeCost;

	public Item OilMaxUpgrade;

	public int MaxLengthLevel => longCosts != null ? longCosts.Length : 5;

	public int MaxOilLevel => oilCosts != null ? oilCosts.Length : 30;

	public int MaxPowerLevel => powerCosts != null ? powerCosts.Length : 15;

	public int MaxSizeLevel => sizeCosts != null ? sizeCosts.Length : 10;

	public int CurrentLengthLevel => _currentLong;

	public int CurrentOilLevel => _currentOil;

	public int CurrentPowerLevel => _currentPower;

	public int CurrentSizeLevel => _currentSize;

	private void Awake()
	{
		longCosts = new int[] { 10, 500, 1000, 4000, 10000 };
		ResolveReferences();
	}

	private void OnEnable()
	{
		_currentLong = PlayerPrefs.GetInt("LengthUpgrade", 0);
		_currentOil = PlayerPrefs.GetInt("OilUpgrade", 0);
		_currentPower = PlayerPrefs.GetInt("PowerUpgrade", 0);
		_currentSize = PlayerPrefs.GetInt("SizeUpgrade", 0);
		UpdateAllButtons();
	}

	public void ResolveReferences()
	{
		if (_moneyClass == null) _moneyClass = Object.FindAnyObjectByType<Money>();
		if (_upgrade == null) _upgrade = Object.FindAnyObjectByType<Upgrade>();

		if (IncrementalLong == null) IncrementalLong = GameObject.Find("Incremental/Long") ?? GameObject.Find("Long");
		if (IncrementalOil == null) IncrementalOil = GameObject.Find("Incremental/Oil") ?? GameObject.Find("Oil");
		if (IncrementalPower == null) IncrementalPower = GameObject.Find("Incremental/Power") ?? GameObject.Find("Power");
		if (IncrementalSize == null) IncrementalSize = GameObject.Find("Incremental/Size") ?? GameObject.Find("Size");

		if (longButton == null && IncrementalLong != null) longButton = IncrementalLong.GetComponentInChildren<Button>(true);
		if (oilButton == null && IncrementalOil != null) oilButton = IncrementalOil.GetComponentInChildren<Button>(true);
		if (powerButton == null && IncrementalPower != null) powerButton = IncrementalPower.GetComponentInChildren<Button>(true);
		if (sizeButton == null && IncrementalSize != null) sizeButton = IncrementalSize.GetComponentInChildren<Button>(true);

		if (_longCost == null && IncrementalLong != null) _longCost = FindCostText(IncrementalLong);
		if (_oilCost == null && IncrementalOil != null) _oilCost = FindCostText(IncrementalOil);
		if (_powerCost == null && IncrementalPower != null) _powerCost = FindCostText(IncrementalPower);
		if (_sizeCost == null && IncrementalSize != null) _sizeCost = FindCostText(IncrementalSize);

		Canvas menuCanvas = IncrementalLong != null ? IncrementalLong.GetComponentInParent<Canvas>() : null;
		if (menuCanvas != null && menuCanvas.sortingOrder < 6)
		{
			menuCanvas.sortingOrder = 6;
		}

		if (IncrementalLong != null && IncrementalLong.transform.parent != null)
		{
			IncrementalLong.transform.parent.gameObject.SetActive(true);
			Image containerImg = IncrementalLong.transform.parent.GetComponent<Image>();
			if (containerImg != null)
			{
				containerImg.raycastTarget = false;
			}
		}
	}

	private float _lastCheckedMoney = -1f;

	private void Update()
	{
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		float money = gp != null ? gp.Money() : 0f;
		if (Mathf.Abs(money - _lastCheckedMoney) > 0.001f)
		{
			_lastCheckedMoney = money;
			UpdateAllButtons();
		}
	}

	private Text FindCostText(GameObject root)
	{
		Text[] texts = root.GetComponentsInChildren<Text>(true);
		foreach (var t in texts)
		{
			string tName = t.gameObject.name.ToLower();
			if (tName.Contains("cost") || tName.Contains("price") || tName.Contains("text"))
			{
				return t;
			}
		}
		return texts.Length > 0 ? texts[0] : null;
	}

	private void Start()
	{
		ResolveReferences();

		_currentLong = PlayerPrefs.GetInt("LengthUpgrade", 0);
		_currentOil = PlayerPrefs.GetInt("OilUpgrade", 0);
		_currentPower = PlayerPrefs.GetInt("PowerUpgrade", 0);
		_currentSize = PlayerPrefs.GetInt("SizeUpgrade", 0);

		if (longButton != null)
		{
			longButton.onClick.RemoveAllListeners();
			longButton.onClick.AddListener(() => BuyLong(false));
		}
		if (oilButton != null)
		{
			oilButton.onClick.RemoveAllListeners();
			oilButton.onClick.AddListener(() => BuyOil(false));
		}
		if (powerButton != null)
		{
			powerButton.onClick.RemoveAllListeners();
			powerButton.onClick.AddListener(() => BuyPower(false));
		}
		if (sizeButton != null)
		{
			sizeButton.onClick.RemoveAllListeners();
			sizeButton.onClick.AddListener(() => BuySize(false));
		}

		// Hide all RV and ForceRV buttons completely
		if (RvlongButton != null) RvlongButton.gameObject.SetActive(false);
		if (RvoilButton != null) RvoilButton.gameObject.SetActive(false);
		if (RvpowerButton != null) RvpowerButton.gameObject.SetActive(false);
		if (RvsizeButton != null) RvsizeButton.gameObject.SetActive(false);
		if (ForceRvPowerButton != null) ForceRvPowerButton.gameObject.SetActive(false);
		if (ForceRvSizeButton != null) ForceRvSizeButton.gameObject.SetActive(false);

		UpdateAllButtons();
	}





	public void UpdateAllButtons()
	{
		_currentLongCost = (longCosts != null && _currentLong < longCosts.Length) ? longCosts[_currentLong] : 99999;
		_currentOilCost = (oilCosts != null && _currentOil < oilCosts.Length) ? oilCosts[_currentOil] : 99999;
		_currentPowerCost = (powerCosts != null && _currentPower < powerCosts.Length) ? powerCosts[_currentPower] : 99999;
		_currentSizeCost = (sizeCosts != null && _currentSize < sizeCosts.Length) ? sizeCosts[_currentSize] : 99999;

		if (_longCost != null) _longCost.text = _currentLong >= MaxLengthLevel ? "MAX" : _currentLongCost.ToString();
		if (_oilCost != null) _oilCost.text = _currentOil >= MaxOilLevel ? "MAX" : _currentOilCost.ToString();
		if (_powerCost != null) _powerCost.text = _currentPower >= MaxPowerLevel ? "MAX" : _currentPowerCost.ToString();
		if (_sizeCost != null) _sizeCost.text = _currentSize >= MaxSizeLevel ? "MAX" : _currentSizeCost.ToString();

		// Always keep the 4 upgrade buttons and their container active so player can upgrade anytime
		if (IncrementalLong != null && IncrementalLong.transform.parent != null)
		{
			IncrementalLong.transform.parent.gameObject.SetActive(true);
		}
		if (IncrementalLong != null) IncrementalLong.SetActive(true);
		if (IncrementalOil != null) IncrementalOil.SetActive(true);
		if (IncrementalPower != null) IncrementalPower.SetActive(true);
		if (IncrementalSize != null) IncrementalSize.SetActive(true);

		// Onboarding / Tutorial free indicators
		bool isFreeLong = false;
		bool isFreeOil = false;
		bool isFreePower = false;
		bool isFreeSize = false;

		if (OnboardingManager.Instance != null && OnboardingManager.Instance.IsOnboardingActive)
		{
			var states = OnboardingManager.Instance.GetIncrementalStates();
			
			isFreeLong = states.LongIsFree;
			isFreeOil = states.OilIsFree;
			isFreePower = states.PowerIsFree;
			isFreeSize = states.SizeIsFree;

			bool showLong = isFreeLong;
			bool showOil = isFreeOil && !showLong;
			bool showPower = isFreePower && !showLong && !showOil;
			bool showSize = isFreeSize && !showLong && !showOil && !showPower;

			if (IncrementalLongOnboardingArea != null) IncrementalLongOnboardingArea.SetActive(showLong);
			if (IncrementalOilOnboardingArea != null) IncrementalOilOnboardingArea.SetActive(showOil);
			if (IncrementalPowerOnboardingArea != null) IncrementalPowerOnboardingArea.SetActive(showPower);
			if (IncrementalSizeOnboardingArea != null) IncrementalSizeOnboardingArea.SetActive(showSize);
		}
		else
		{
			if (IncrementalLongOnboardingArea != null) IncrementalLongOnboardingArea.SetActive(false);
			if (IncrementalOilOnboardingArea != null) IncrementalOilOnboardingArea.SetActive(false);
			if (IncrementalPowerOnboardingArea != null) IncrementalPowerOnboardingArea.SetActive(false);
			if (IncrementalSizeOnboardingArea != null) IncrementalSizeOnboardingArea.SetActive(false);
		}

		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		float money = gp != null ? gp.Money() : 0f;

		if (longButton != null) longButton.interactable = _currentLong < MaxLengthLevel && (money >= _currentLongCost || isFreeLong);
		if (oilButton != null) oilButton.interactable = _currentOil < MaxOilLevel && (money >= _currentOilCost || isFreeOil);
		if (powerButton != null) powerButton.interactable = _currentPower < MaxPowerLevel && (money >= _currentPowerCost || isFreePower);
		if (sizeButton != null) sizeButton.interactable = _currentSize < MaxSizeLevel && (money >= _currentSizeCost || isFreeSize);
	}

	private int _lastBuyLongFrame = -1;
	private float _lastBuyLongTime = -1f;

	public void BuyLong(bool byRV = false)
	{
		if (Time.frameCount == _lastBuyLongFrame || (Time.unscaledTime - _lastBuyLongTime < 0.25f && _lastBuyLongTime > 0f)) return;
		_lastBuyLongFrame = Time.frameCount;
		_lastBuyLongTime = Time.unscaledTime;

		if (_currentLong >= MaxLengthLevel) return;
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		bool isFree = OnboardingManager.Instance != null && OnboardingManager.Instance.GetIncrementalStates().LongIsFree;

		if (byRV || isFree || (gp != null && gp.Money() >= _currentLongCost))
		{
			if (!byRV && !isFree && gp != null) gp.SetMoney(-_currentLongCost);
			_currentLong++;
			PlayerPrefs.SetInt("LengthUpgrade", _currentLong);
			PlayerPrefs.Save();
			if (_upgrade == null) _upgrade = Object.FindAnyObjectByType<Upgrade>();
			if (_upgrade != null) _upgrade.UpdateLong();
			UpdateAllButtons();
		}
	}

	private void BuyLongByRV(bool success)
	{
		if (success) BuyLong(true);
	}

	private int _lastBuyOilFrame = -1;
	private float _lastBuyOilTime = -1f;

	public void BuyOil(bool byRV = false)
	{
		if (Time.frameCount == _lastBuyOilFrame || (Time.unscaledTime - _lastBuyOilTime < 0.25f && _lastBuyOilTime > 0f)) return;
		_lastBuyOilFrame = Time.frameCount;
		_lastBuyOilTime = Time.unscaledTime;

		if (_currentOil >= MaxOilLevel) return;
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		bool isFree = OnboardingManager.Instance != null && OnboardingManager.Instance.GetIncrementalStates().OilIsFree;

		if (byRV || isFree || (gp != null && gp.Money() >= _currentOilCost))
		{
			if (!byRV && !isFree && gp != null) gp.SetMoney(-_currentOilCost);
			_currentOil++;
			PlayerPrefs.SetInt("OilUpgrade", _currentOil);
			PlayerPrefs.Save();
			if (_upgrade == null) _upgrade = Object.FindAnyObjectByType<Upgrade>();
			if (_upgrade != null) _upgrade.UpdateOil();
			UpdateAllButtons();
		}
	}

	private void BuyOilByRV(bool success)
	{
        CheckCpAd.instance.CallAd();
        if (success) BuyOil(true);
	}

	private int _lastBuyPowerFrame = -1;
	private float _lastBuyPowerTime = -1f;

	public void BuyPower(bool byRV = false)
	{
        CheckCpAd.instance.CallAd();
        if (Time.frameCount == _lastBuyPowerFrame || (Time.unscaledTime - _lastBuyPowerTime < 0.25f && _lastBuyPowerTime > 0f)) return;
		_lastBuyPowerFrame = Time.frameCount;
		_lastBuyPowerTime = Time.unscaledTime;

		if (_currentPower >= MaxPowerLevel) return;
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		bool isFree = OnboardingManager.Instance != null && OnboardingManager.Instance.GetIncrementalStates().PowerIsFree;

		if (byRV || isFree || (gp != null && gp.Money() >= _currentPowerCost))
		{
			if (!byRV && !isFree && gp != null) gp.SetMoney(-_currentPowerCost);
			_currentPower++;
			PlayerPrefs.SetInt("PowerUpgrade", _currentPower);
			PlayerPrefs.Save();
			if (_upgrade == null) _upgrade = Object.FindAnyObjectByType<Upgrade>();
			if (_upgrade != null) _upgrade.UpdatePower();
			UpdateAllButtons();
		}
	}

	private void BuyPowerByRV(bool success, bool free)
	{
		if (success) BuyPower(true);
	}

	public void BuyPowerByForceRV()
	{
		BuyPower(true);
	}

	private int _lastBuySizeFrame = -1;
	private float _lastBuySizeTime = -1f;

	public void BuySize(bool byRV = false)
	{
        CheckCpAd.instance.CallAd();
        if (Time.frameCount == _lastBuySizeFrame || (Time.unscaledTime - _lastBuySizeTime < 0.25f && _lastBuySizeTime > 0f)) return;
		_lastBuySizeFrame = Time.frameCount;
		_lastBuySizeTime = Time.unscaledTime;

		if (_currentSize >= MaxSizeLevel) return;
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		bool isFree = OnboardingManager.Instance != null && OnboardingManager.Instance.GetIncrementalStates().SizeIsFree;

		if (byRV || isFree || (gp != null && gp.Money() >= _currentSizeCost))
		{
			if (!byRV && !isFree && gp != null) gp.SetMoney(-_currentSizeCost);
			_currentSize++;
			PlayerPrefs.SetInt("SizeUpgrade", _currentSize);
			PlayerPrefs.Save();
			if (_upgrade == null) _upgrade = Object.FindAnyObjectByType<Upgrade>();
			if (_upgrade != null) _upgrade.UpdateSize();
			UpdateAllButtons();
		}
	}

	private void BuySizeByRV(bool success, bool free)
	{
        CheckCpAd.instance.CallAd();
        if (success) BuySize(true);
	}

	public void BuySizeByForceRV()
	{
		BuySize(true);
	}
}
