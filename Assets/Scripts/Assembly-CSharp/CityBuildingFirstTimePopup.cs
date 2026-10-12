using UnityEngine;

public class CityBuildingFirstTimePopup : MonoBehaviour
{
	public bool ShowInEditor;

	public GameObject Root;

	public const string FIRST_DONE = "VoodooCBFirstTime";

	public static bool HasBeenShown
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	public void Hide()
	{
	}
}
