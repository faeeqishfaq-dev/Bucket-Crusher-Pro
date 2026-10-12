using UnityEngine;

public class InputSwitch : MonoBehaviour
{
	public Joystick DefaultJoystick;

	public Joystick NewJoystick;

	public GameObject TapPanel;

	public Canvas thisCanvas;

	private void Awake()
	{
		ApplyInputMode();
	}

	private void Start()
	{
		ApplyInputMode();
	}

	public void ApplyInputMode()
	{
		if (NewJoystick != null)
		{
			NewJoystick.gameObject.SetActive(true);
		}
		if (DefaultJoystick != null && DefaultJoystick != NewJoystick)
		{
			DefaultJoystick.gameObject.SetActive(false);
		}
		if (TapPanel != null)
		{
			TapPanel.SetActive(false);
		}
		if (thisCanvas != null)
		{
			thisCanvas.gameObject.SetActive(true);
		}
	}
}
