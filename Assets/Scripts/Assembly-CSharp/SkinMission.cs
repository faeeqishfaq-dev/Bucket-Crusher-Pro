using UnityEngine;

[CreateAssetMenu(menuName = "Shop/SkinMission", fileName = "SkinMission")]
public class SkinMission : ScriptableObject
{
	public string ID;

	public SkinMissionType MissionType;

	public string Description;

	public int Goal;

	public Item LinkedSkin;

	public int Progress
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public string GetStatus()
	{
		return null;
	}
}
