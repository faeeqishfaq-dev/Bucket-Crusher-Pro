using UnityEngine;

public class CheatsManager : MonoBehaviour
{
	public GameObject[] CheatsPrefabs;

	private static CheatsManager _instance;

	private bool _initialized;

	public static CheatsManager Instance => null;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnDebugButtonClicked(bool appear)
	{
	}

	public int GetCurrentLevel()
	{
		return 0;
	}

	public void SetLevel(int val)
	{
	}

	public void AddLevel(int amount)
	{
	}

	public int GetCurrentCoins()
	{
		return 0;
	}

	public void SetCoins(int val)
	{
	}

	public void AddCoins(int amount)
	{
	}

	public int GetCurrentGems()
	{
		return 0;
	}

	public void SetGems(int val)
	{
	}

	public void AddGems(int amount)
	{
	}

	public void Reload()
	{
	}
}
