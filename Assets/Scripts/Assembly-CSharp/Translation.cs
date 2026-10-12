using UnityEngine;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
	private string _lKode;

	[SerializeField]
	private Font _baseFont;

	[SerializeField]
	private Font _rusFont;

	[Space]
	[SerializeField]
	private Text[] Generic_NoThanks;

	[SerializeField]
	private Text[] Generic_Play;

	[SerializeField]
	private Text[] Generic_Back;

	[SerializeField]
	private Text[] Generic_OutOfFuel;

	[SerializeField]
	private Text[] Generic_LevelCompleted;

	[SerializeField]
	private Text[] Generic_Free;

	[SerializeField]
	private Text[] Generic_Skins;

	[SerializeField]
	private Text[] Generic_Upgrades;

	[SerializeField]
	private Text[] Generic_Max;

	[SerializeField]
	private Text[] Generic_Unlocked;

	[SerializeField]
	private Text[] Generic_New;

	[SerializeField]
	private Text[] Generic_FillTank;

	[Space]
	[SerializeField]
	private Text[] NoInternetPopup;

	[Space]
	[SerializeField]
	private Text[] Options_PrivacyPolicy;

	[SerializeField]
	private Text[] Options_Restore;

	[SerializeField]
	private Text[] Options_RestoreFailed;

	[SerializeField]
	private Text[] Options_RestoreSuccess;

	[Space]
	[SerializeField]
	private Text[] Noads_Header;

	[SerializeField]
	private Text[] Noads_Explanation;

	[SerializeField]
	private Text[] Noads_ThankYou;

	[SerializeField]
	private Text[] Noads_Buy;

	[Space]
	[SerializeField]
	private Text[] Increment_Length;

	[SerializeField]
	private Text[] Increment_Fuel;

	[SerializeField]
	private Text[] Increment_Power;

	[SerializeField]
	private Text[] Increment_Size;

	[Space]
	[SerializeField]
	private Text[] RocketLauncher;

	[Space]
	[SerializeField]
	private Text[] BonusLevel_Header;

	[SerializeField]
	private Text[] BonusLevel_Go;

	[SerializeField]
	private Text[] BonusLevel_Description;

	[Space]
	[SerializeField]
	private Text[] SkinUnlockedPopup_Header;

	[SerializeField]
	private Text[] SkinUnlockedPopup_Select;

	[SerializeField]
	private Text[] CashBoost;

	[SerializeField]
	private Text[] DrillBoost;

	[Space]
	[SerializeField]
	private Text[] Generic_Ok;

	[SerializeField]
	private Image[] _noads;

	[SerializeField]
	private Sprite[] _NoAdsSprites;

	private void Start()
	{
	}

	private void Translate(Text[] t, string key)
	{
	}
}
