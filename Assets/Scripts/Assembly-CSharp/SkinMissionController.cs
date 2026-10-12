using System.Collections.Generic;
using Voodoo.Pattern;

public class SkinMissionController : SingletonMB<SkinMissionController>
{
	public List<SkinMission> AllMissions;

	public static string UnlockedItem
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnGameStart()
	{
	}

	public SkinMission GetMission(Item item)
	{
		return null;
	}

	public void ProgressMission(SkinMissionType missionType, int amount)
	{
	}

	internal string GetText(Item item)
	{
		return null;
	}
}
