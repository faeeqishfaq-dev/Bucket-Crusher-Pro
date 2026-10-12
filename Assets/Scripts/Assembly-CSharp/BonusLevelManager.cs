using UnityEngine;
using UnityEngine.UI;
using Voodoo.Pattern;

public class BonusLevelManager : SingletonMB<BonusLevelManager>
{
	public bool ForceBonusLevel;

	[SerializeField]
	private GameObject _RVIcon;

	[SerializeField]
	private GameObject _noThanksButton;

	[SerializeField]
	private Image _bonusLevelIcon;

	private int m_BonusLevelReturnId;

	public bool CanShowBonusLevelRV { get; private set; }

	public bool IsBonusLevelActive => false;

	private int GamesSinceLastBonusLevel
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	private int NumberBonusLevelActivated
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public void Start()
	{
	}

	private void OnGameEnded()
	{
	}

	private void OnGameStarted()
	{
	}

	public void OnRVClicked()
	{
	}
}
