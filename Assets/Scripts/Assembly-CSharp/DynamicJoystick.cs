using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
	[SerializeField]
	private float moveThreshold = 1f;

	public float MoveThreshold
	{
		get => moveThreshold;
		set => moveThreshold = Mathf.Abs(value);
	}

	protected override void Start()
	{
		MoveThreshold = moveThreshold;
		base.Start();
		if (background != null)
		{
			background.gameObject.SetActive(false);
		}
	}

		public override void OnPointerDown(PointerEventData eventData)
	{
		if (IsPointerOverMultiplierButton(eventData.position)) return;

		if (isPointerDown) return;

		if (background != null)
		{
			background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
			background.gameObject.SetActive(true);
		}
		base.OnPointerDown(eventData);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if (!isPointerDown) return;
		if (eventData.pointerId != activePointerId) return;
		if (background != null)
		{
			background.gameObject.SetActive(false);
		}
		base.OnPointerUp(eventData);
	}

	protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
	{
		if (magnitude > moveThreshold)
		{
			Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
			if (background != null)
			{
				background.anchoredPosition += difference;
			}
		}
		base.HandleInput(magnitude, normalised, radius, cam);
	}
}
