using System;
using System.Collections.Generic;
using UnityEngine;

public class CityBuildingManager : MonoBehaviour
{
	public Action<CityBuildingElementHandler> OnElementSelected;

	public Action<bool> OnRVX2;

	public Action OnCollectGems;

	public List<GameObject> Sounds;

	public Sprite GemSprite;

	public Sprite CashSprite;

	public LayerMask LayerMask;

	public UIAudio UIAudio;

	public GameplayAudio GameplayAudio;

	private static CityBuildingManager _instance;

	public const float MULTIPLY_DURATION_MINUTES = 5f;

	public const int MULTIPLY_VALUE = 3;

	public const string X2_RVTAKENDATE = "VoodooCBX2RV";

	private bool _rvX2Enable;

	private CityBuildingElementHandler _currentClickedHandler;

	private Vector2 _screenStart;

	public bool SoundEnable => false;

	public int Money
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int Gems
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int CurrentLevel
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public static CityBuildingManager Instance => null;

	public DateTime LastX2CollectPref
	{
		get
		{
			return default(DateTime);
		}
		set
		{
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void DoHaptic()
	{
	}

	public void DoLightHaptic()
	{
	}

	[ExposeMethodInEditor(false, false)]
	private void GiveMoney()
	{
	}

	[ExposeMethodInEditor(false, false)]
	private void GiveGems()
	{
	}

	[ExposeMethodInEditor(false, false)]
	private void AddLevel()
	{
	}

	[ExposeMethodInEditor(false, false)]
	private void DoReset()
	{
	}

	[ExposeMethodInEditor(false, false)]
	private void Refresh()
	{
	}

	public void Select(CityBuildingElementHandler element)
	{
	}

	public void OnX2GenerationClicked()
	{
	}

	public bool IsX2Enable()
	{
		return false;
	}

	private void Update()
	{
	}

	public bool IsOverUI()
	{
		return false;
	}
}
