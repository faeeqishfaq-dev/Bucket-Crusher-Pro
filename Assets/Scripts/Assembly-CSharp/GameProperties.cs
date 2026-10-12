using UnityEngine;
using Object = UnityEngine.Object;

public class GameProperties : MonoBehaviour
{
	private Money _wallet;

	private void Awake()
	{
		_wallet = Object.FindAnyObjectByType<Money>();
	}

	public float Money()
	{
		return PlayerPrefs.GetFloat("Money", 50f);
	}

	public void SetMoney(float delta)
	{
		float current = Money() + delta;
		if (current < 0f) current = 0f;
		PlayerPrefs.SetFloat("Money", current);
		PlayerPrefs.Save();
		if (_wallet != null)
		{
			_wallet.UpdateValue();
		}
	}

	public int Level()
	{
		return PlayerPrefs.GetInt("Level", 1);
	}

	public void NextLevel()
	{
		int lvl = Level() + 1;
		PlayerPrefs.SetInt("Level", lvl);
		PlayerPrefs.Save();
	}

	public bool IsSoundEnable()
	{
		return PlayerPrefs.GetInt("Sound", 1) == 1;
	}

	public void SetSoundEnable(bool on)
	{
		PlayerPrefs.SetInt("Sound", on ? 1 : 0);
		PlayerPrefs.Save();
	}

	public bool IsHapticEnable()
	{
		return PlayerPrefs.GetInt("Haptic", 1) == 1;
	}

	public void SetHapticEnable(bool on)
	{
		PlayerPrefs.SetInt("Haptic", on ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void RateUsShown()
	{
		PlayerPrefs.SetInt("RateUsShown", 1);
		PlayerPrefs.Save();
	}

	public bool WasRateUsShown()
	{
		return PlayerPrefs.GetInt("RateUsShown", 0) == 1;
	}
}
