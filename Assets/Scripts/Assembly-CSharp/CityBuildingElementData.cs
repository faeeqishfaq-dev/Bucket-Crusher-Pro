using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CityBuildingElementData", menuName = "Voodoo/CityBuildingElementData", order = 1)]
public class CityBuildingElementData : ScriptableObject
{
	public Action OnUnlock;

	public int UnlockLevel;

	public int UnlockPrice;

	public string ID;

	public bool GiveGems;

	public float GainPerMinute;

	public const string LAST_COLLECT = "VoodooCBCollect";

	private DateTime _lastCollect;

	public const string IS_UNLOCKED = "VoodooCBUnlocked";

	private bool _unlocked;

	public const string CURRENT_LEVEL = "VoodooCBLevel";

	private int _level;

	public bool Unlocked
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public DateTime LastCollect
	{
		get
		{
			return default(DateTime);
		}
		set
		{
		}
	}

	public int MoneyToCollect => 0;

	public int Level
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	private DateTime _lastCollectPref
	{
		get
		{
			return default(DateTime);
		}
		set
		{
		}
	}

	private bool _unlockedPref
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private int _levelPref
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int GainPerHours => 0;

	public string GainPerHoursString => null;

	public int NextLevelGainPerHours => 0;

	public string NextLevelGainPerHoursString => null;

	public void Initialize()
	{
	}

	public bool IsRewardAvailable()
	{
		return false;
	}

	public bool ShowNotification()
	{
		return false;
	}

	public int UpgradePrice()
	{
		return 0;
	}
}
