using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
	[SerializeField]
	private float moveThreshold;

	[SerializeField]
	private JoystickType joystickType;

	private Vector2 fixedPosition;

	public float MoveThreshold
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public void SetMode(JoystickType joystickType)
	{
	}

	protected override void Start()
	{
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
	}

	protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
	{
	}
}
