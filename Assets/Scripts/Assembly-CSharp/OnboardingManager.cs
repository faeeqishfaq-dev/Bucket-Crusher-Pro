using UnityEngine;
using Voodoo.Pattern;

public class OnboardingManager : SingletonMB<OnboardingManager>
{
	public struct IncrementalButtonStates
	{
		public bool LongVisible;
		public bool LongIsFree;
		public bool OilVisible;
		public bool OilIsFree;
		public bool PowerVisible;
		public bool PowerIsFree;
		public bool SizeVisible;
		public bool SizeIsFree;
	}

	public int LongStartAtGame = 1;

	public int OilStartAtGame = 2;

	public int PowerStartAtGame = 3;

	public int SizeStartAtGame = 3;

	public int GetNumberGamePlayed
	{
		get => PlayerPrefs.GetInt("NumberGamePlayed", 0);
		set
		{
			PlayerPrefs.SetInt("NumberGamePlayed", value);
			PlayerPrefs.Save();
		}
	}

	public int AllIncrementalAccessibleAtGame => 3;

	public bool IsOnboardingActive
	{
		get
		{
			int level = GameManager.Instance != null ? GameManager.Instance.Currentlevel : PlayerPrefs.GetInt("Level", 1);
			return level < 3 && GetNumberGamePlayed < AllIncrementalAccessibleAtGame;
		}
	}

	private void Start()
	{
	}

	public IncrementalButtonStates GetIncrementalStates()
	{
		int games = GetNumberGamePlayed;
		IncrementalButtonStates states = new IncrementalButtonStates();

		states.LongVisible = games >= LongStartAtGame;
		states.OilVisible = games >= OilStartAtGame;
		states.PowerVisible = games >= PowerStartAtGame;
		states.SizeVisible = games >= SizeStartAtGame;

		states.LongIsFree = games == LongStartAtGame && PlayerPrefs.GetInt("LengthUpgrade", 0) == 0;
		states.OilIsFree = games == OilStartAtGame && PlayerPrefs.GetInt("OilUpgrade", 0) == 0;
		states.PowerIsFree = games == PowerStartAtGame && PlayerPrefs.GetInt("PowerUpgrade", 0) == 0;
		states.SizeIsFree = games == SizeStartAtGame && PlayerPrefs.GetInt("SizeUpgrade", 0) == 0;

		return states;
	}
}
