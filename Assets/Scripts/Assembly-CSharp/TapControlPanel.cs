using UnityEngine;
using UnityEngine.EventSystems;

public class TapControlPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerExitHandler
{
	private Joystick _joystick;

	private void Awake()
	{
		EnsureJoystick();
	}

	private void EnsureJoystick()
	{
		if (_joystick == null)
		{
			_joystick = Object.FindAnyObjectByType<DynamicJoystick>();
			if (_joystick == null) _joystick = Object.FindAnyObjectByType<Joystick>();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		EnsureJoystick();
		if (_joystick != null) _joystick.OnPointerDown(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		EnsureJoystick();
		if (_joystick != null) _joystick.OnDrag(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		EnsureJoystick();
		if (_joystick != null) _joystick.OnPointerUp(eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		EnsureJoystick();
		if (_joystick != null) _joystick.OnPointerUp(eventData);
	}
}
