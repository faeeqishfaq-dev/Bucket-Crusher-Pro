using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Voodoo.Pattern;
using Voodoo.Pattern.Random;

public class GameManager : SingletonMB<GameManager>
{
	[SerializeField]
	private GameObject _menu;

	[SerializeField]
	private GameObject _game;

	[SerializeField]
	private GameObject _outOf;

	[SerializeField]
	private GameObject _outOfOilRv;

	[SerializeField]
	private GameObject _outOfMoneyRv;

	[SerializeField]
	private GameObject _bonusLevelRv;

	[SerializeField]
	private GameObject _bonusLevelBanner;

	[SerializeField]
	private Animator _camAnim;

	[SerializeField]
	private RectTransform _joy;

	[SerializeField]
	public bool IsDuelMode;

	[SerializeField]
	private GameObject _finish;

	[SerializeField]
	private Transform _sky;

	[SerializeField]
	private Image _1;

	[SerializeField]
	private Image _2;

	[SerializeField]
	private Text _3;

	[SerializeField]
	private Text _earned;

	[SerializeField]
	private GameObject _duelModeUnlocked;

	public GameObject PlayButton;

	public Oil currentOilUI;

	//private Ads _ads;

	public Cam Cam;

	public bool finised;
	public bool IsPlaying { get; private set; }
	

	private int _gameCount;

	public CoinFxReceiver _CashReceiver;

	private const string GAME_COUNT_PREF = "VoodooBucketGameCount";

	public Control Control;

	public GameplayAudio GameplayAudio;

	public UIAudio UIAudio;

	public GameplayHaptic GameplayHaptic;

	private GameProperties gameProperties;

	public Action OnGameStart;

	public Action OnGameEnd;

	public bool BoostLevel;

	private const string c_IsDrawingLevelKey = "IsDrawingLevelKey";

	public PseudoPool namePool;

	private AsyncOperation _asyncTransition;

	public bool EcoMode;

	public float EcoTimer = 30f;

	public bool BetterNoAds => false;

	public Joystick Joystick { get; internal set; }

	public int Currentlevel => gameProperties != null ? gameProperties.Level() : PlayerPrefs.GetInt("Level", 1);

	public Action OnBoughtBetterNoAds { get; internal set; }

	private GameObject _inputCanvas;

	public void SetInputCanvasActive(bool active)
	{
		if (_inputCanvas == null)
		{
			Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
			foreach (var c in canvases)
			{
				if (c != null && c.gameObject != null && c.gameObject.name == "InputCanvas")
				{
					_inputCanvas = c.gameObject;
					break;
				}
			}
		}
		if (_inputCanvas != null)
		{
			_inputCanvas.SetActive(active);
		}
	}

	private void Awake()
	{
		gameProperties = GetComponent<GameProperties>();
		if (gameProperties == null) gameProperties = Object.FindAnyObjectByType<GameProperties>();
		if (Control == null) Control = Object.FindAnyObjectByType<Control>();
		if (Cam == null) Cam = Object.FindAnyObjectByType<Cam>();
		if (currentOilUI == null) currentOilUI = Object.FindAnyObjectByType<Oil>();
		if (GameplayAudio == null) GameplayAudio = Object.FindAnyObjectByType<GameplayAudio>();
		if (UIAudio == null) UIAudio = Object.FindAnyObjectByType<UIAudio>();
		if (GameplayHaptic == null) GameplayHaptic = Object.FindAnyObjectByType<GameplayHaptic>();
		if (Joystick == null) Joystick = Object.FindAnyObjectByType<Joystick>();
	}

	private void Start()
	{
		// Kept EndScreenView for Level Complete
		var endScreen = UnityEngine.Object.FindAnyObjectByType<EndScreenView>(FindObjectsInactive.Include);
		finised = false;
		if (_menu != null) _menu.SetActive(true);
		SetStartMenuDecorationsActive(true);
		if (_game != null) _game.SetActive(false);
		if (_outOf != null) _outOf.SetActive(false);
		if (_finish != null) _finish.SetActive(false);
		SetInputCanvasActive(false);

		if (PlayButton != null)
		{
			Button[] btns = PlayButton.GetComponentsInChildren<Button>(true);
			foreach (var btn in btns)
			{
				btn.onClick.RemoveListener(StartGame);
				btn.onClick.AddListener(StartGame);
			}
		}

		if (Cam != null) Cam.ShowWholeLevel();

		Joystick joy = UnityEngine.Object.FindAnyObjectByType<DynamicJoystick>();
		if (joy == null) joy = UnityEngine.Object.FindAnyObjectByType<Joystick>();
		if (joy != null) ReplaceControls(joy);
	}

	public void SetStartMenuDecorationsActive(bool active)
	{
		if (_menu != null)
		{
			Canvas c = _menu.GetComponentInParent<Canvas>();
			if (c != null && c.sortingOrder < 6)
			{
				c.sortingOrder = 6;
			}

			foreach (Transform child in _menu.transform)
			{
				if (child.name.StartsWith("Image") || child.name == "StartButtonGroup")
				{
					child.gameObject.SetActive(active);
				}
				else if (child.name == "Incremental")
				{
					child.gameObject.SetActive(true);
				}
			}
		}

		if (PlayButton != null)
		{
			PlayButton.SetActive(active);
			if (PlayButton.transform.parent != null && PlayButton.transform.parent.name == "StartButtonGroup")
			{
				PlayButton.transform.parent.gameObject.SetActive(active);
			}
		}

		if (Shop.Instance != null)
		{
			Shop.Instance.UpdateAllButtons();
		}
	}

	private void Update()
	{
		if (PlayButton != null && PlayButton.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			{
				StartGame();
			}
		}
	}

	public void StartGame()
	{
				SetStartMenuDecorationsActive(false);
		if (_game != null) _game.SetActive(true);
		if (_outOf != null) _outOf.SetActive(false);
		if (_finish != null) _finish.SetActive(false);
		SetInputCanvasActive(true);

		finised = false;

		Joystick joy = UnityEngine.Object.FindAnyObjectByType<DynamicJoystick>();
		if (joy == null) joy = UnityEngine.Object.FindAnyObjectByType<Joystick>();
		if (joy != null) ReplaceControls(joy);

		if (currentOilUI == null) currentOilUI = UnityEngine.Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);
		if (currentOilUI != null)
		{
			currentOilUI.StartSpendingOil();
		}


		if (Cam != null)
		{
			Cam.ForceFollowCustom();
		}

		if (_camAnim != null)
		{
			bool hasPlay = false;
			foreach (var p in _camAnim.parameters)
			{
				if (p.name == "Play")
				{
					hasPlay = true;
					break;
				}
			}
			if (hasPlay)
			{
				_camAnim.SetTrigger("Play");
			}
		}

		IsPlaying = true;
		Shop s = UnityEngine.Object.FindAnyObjectByType<Shop>();
		if (s != null) s.UpdateAllButtons();
		OnGameStart?.Invoke();
	}

	public void EndGame()
	{
		Debug.Log($"[GamePlayTest] GameManager.EndGame() executing! finised={finised}");
		if (finised) return;

		SetInputCanvasActive(false);

		if (currentOilUI == null) currentOilUI = UnityEngine.Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);
		if (currentOilUI != null)
		{
			currentOilUI.StopSpend();
		}

		if (OnboardingManager.Instance != null)
		{
			OnboardingManager.Instance.GetNumberGamePlayed++;
		}

		OnGameEnd?.Invoke();
		BackToMenu();
	}

	public void FinishLevel()
	{
		if (finised) return;
		finised = true;

		SetInputCanvasActive(false);

		if (currentOilUI != null)
		{
			currentOilUI.StopSpend();
		}

		var endScreen = UnityEngine.Object.FindAnyObjectByType<EndScreenView>(FindObjectsInactive.Include);
		if (endScreen != null)
		{
			endScreen.ShowScreen(EndScreenMode.LevelCompelete, null, () => 
			{
				StartNextLevelSequence();
			});
		}
		else
		{
			StartNextLevelSequence();
		}
	}

	public void StartNextLevelSequence()
	{
		if (gameProperties != null)
		{
			gameProperties.NextLevel();
		}

		OnGameEnd?.Invoke();
        CheckCpAd.instance.CallAd();
        // Reload the scene to generate the new level map and reset the player
        LoadIntoMain();
	}

	public void FinishGame()
	{
		FinishLevel();
	}

	public void BackToMenu()
	{
        CheckCpAd.instance.CallAd();
        IsPlaying = false;
		finised = false;

		SetInputCanvasActive(false);

		if (_outOf != null) _outOf.SetActive(false);
		if (_finish != null) _finish.SetActive(false);
		if (_game != null) _game.SetActive(false);
		if (_menu != null) _menu.SetActive(true);
		SetStartMenuDecorationsActive(true);

		if (currentOilUI != null)
		{
			currentOilUI.StopSpend();
			currentOilUI.UpdateValues();
		}

		if (Cam != null)
		{
			Cam.ShowWholeLevel();
		}

		Shop s = Object.FindAnyObjectByType<Shop>();
		if (s != null) s.UpdateAllButtons();

		Money m = Object.FindAnyObjectByType<Money>();
		if (m != null) m.UpdateValue();
	}

	public void FIllTank()
	{
        CheckCpAd.instance.CallAd();
        finised = false;
		if (currentOilUI != null)
		{
			currentOilUI.AddOil(Mathf.RoundToInt(currentOilUI.MaxOil));
		}
		if (_outOf != null) _outOf.SetActive(false);
		if (_game != null) _game.SetActive(true);
		SetInputCanvasActive(true);
		if (currentOilUI != null) currentOilUI.StartSpendingOil();
	}

	public void GetXMoney()
	{
		if (gameProperties != null)
		{
			gameProperties.SetMoney(100f);
		}
		BackToMenu();
	}

	public void ReplaceControls(Joystick joystick, bool tapControl = false)
	{
		Joystick = joystick;
		if (Control != null) Control.ReplaceJoystick(joystick);
		if (currentOilUI != null) currentOilUI.Joystick = joystick;
	}

	public void SetCameraAnimationSide(LevelPosType type = LevelPosType.Right)
	{
	}

	private void FirstLaunch()
	{
	}

	public void OilLostNoRV()
	{
		BackToMenu();
	}

	public void OilLostOilRV()
	{
		FIllTank();
	}

	public void ShowBonusLevelRVScreen()
	{
	}

	public void LoadIntoDuel()
	{
	}

	public void LoadIntoMain()
	{
		SceneManager.LoadScene("GamePlay");
	}

	internal void StartLoadIntoTransition()
	{
	}

	public void LoadIntoTransition()
	{
	}

	internal void OpponentUpgraded()
	{
	}

	internal void ActivateRenewable()
	{
	}

	private IEnumerator WaitForCameraAnim()
	{
		yield return new WaitForSeconds(1f);
	}

	public void PlayerUpgraded()
	{
	}

	private void TrackOnGameFinish()
	{
	}

	private void ShowLoadingsScreen()
	{
	}

	public void TryShowSkinUnlock()
	{
	}

	public void TryShowDuelUnlocked()
	{
	}

	private void ResetProgress()
	{
		Progress p = Progress.Instance;
		if (p != null) p.ResetCount();
	}

	public void FinishBonusLevel()
	{
		FinishLevel();
	}
}
