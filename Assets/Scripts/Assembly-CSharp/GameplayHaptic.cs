#pragma warning disable CS0414
using UnityEngine;

public class GameplayHaptic : MonoBehaviour, ISwichHaptic
{
	private float hapticTimer;

	private bool canPlay = true;

	public void Update()
	{
	}

	public void PlayHaptic(HapticType type)
	{
	}

	public void HapticOff()
	{
		canPlay = false;
	}

	public void HapticOn()
	{
		canPlay = true;
	}
}
