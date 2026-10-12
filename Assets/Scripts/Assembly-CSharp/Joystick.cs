using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler, IPointerUpHandler, MoveDirection
{
	[SerializeField]
	private float handleRange = 1f;

	[SerializeField]
	private float deadZone = 0f;

	[SerializeField]
	private AxisOptions axisOptions = AxisOptions.Both;

	[SerializeField]
	private bool snapX = false;

	[SerializeField]
	private bool snapY = false;

	[SerializeField]
	protected RectTransform background;

	[SerializeField]
	private RectTransform handle;

	private RectTransform baseRect;

	private Canvas canvas;

	private Camera cam;

	private Vector2 input = Vector2.zero;

	protected int activePointerId = -1;

	protected bool isPointerDown = false;

	public float Horizontal => snapX ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x;

	public float Vertical => snapY ? SnapFloat(input.y, AxisOptions.Vertical) : input.y;

	public Vector2 Direction
	{
		get
		{
			Vector2 dir = new Vector2(Horizontal, Vertical);
			if (dir.sqrMagnitude > 0.001f)
			{
				return dir;
			}

			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
				{
					float midX = Screen.width * 0.5f;
					float x = t.position.x < midX ? -1f : 1f;
					return new Vector2(x, 0f);
				}
			}

			// Keyboard fallback
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			if (h != 0f || v != 0f)
			{
				dir = new Vector2(h, v).normalized;
			}
			return dir;
		}
	}

	public float HandleRange
	{
		get => handleRange;
		set => handleRange = Mathf.Abs(value);
	}

	public float DeadZone
	{
		get => deadZone;
		set => deadZone = Mathf.Abs(value);
	}

	public AxisOptions AxisOptions
	{
		get => axisOptions;
		set => axisOptions = value;
	}

	public bool SnapX
	{
		get => snapX;
		set => snapX = value;
	}

	public bool SnapY
	{
		get => snapY;
		set => snapY = value;
	}

	protected virtual void Start()
	{
		HandleRange = handleRange;
		DeadZone = deadZone;
		baseRect = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			cam = canvas.worldCamera;
		}
		else
		{
			cam = null;
		}

		Vector2 center = new Vector2(0.5f, 0.5f);
		if (baseRect != null)
		{
			baseRect.pivot = center;
		}
		if (background != null)
		{
			background.anchorMin = center;
			background.anchorMax = center;
			background.pivot = center;
		}
		if (handle != null)
		{
			handle.anchorMin = center;
			handle.anchorMax = center;
			handle.pivot = center;
			handle.anchoredPosition = Vector2.zero;
		}
	}

		public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (IsPointerOverMultiplierButton(eventData.position)) return;

		if (isPointerDown) return;

		isPointerDown = true;
		activePointerId = eventData.pointerId;
		OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isPointerDown) return;
		if (eventData.pointerId != activePointerId) return;

		cam = null;
		if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			cam = canvas.worldCamera;
		}

		if (background == null) return;
		Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
		Vector2 radius = background.sizeDelta / 2f;
		float scale = canvas != null ? canvas.scaleFactor : 1f;
		if (radius.x > 0f && scale > 0f)
		{
			input = (eventData.position - position) / (radius * scale);
			FormatInput();
			HandleInput(input.magnitude, input.normalized, radius, cam);
		}
		if (handle != null && radius.x > 0f)
		{
			handle.anchoredPosition = input * radius * handleRange;
		}
	}

	protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
	{
		if (magnitude > deadZone)
		{
			if (magnitude > 1f)
			{
				input = normalised;
			}
		}
		else
		{
			input = Vector2.zero;
		}
	}

	private void FormatInput()
	{
		if (axisOptions == AxisOptions.Horizontal)
		{
			input = new Vector2(input.x, 0f);
		}
		else if (axisOptions == AxisOptions.Vertical)
		{
			input = new Vector2(0f, input.y);
		}
	}

	private float SnapFloat(float value, AxisOptions snapAxis)
	{
		if (value == 0f) return 0f;
		if (axisOptions == AxisOptions.Both)
		{
			float angle = Vector2.Angle(input, Vector2.up);
			if (snapAxis == AxisOptions.Horizontal)
			{
				if (angle < 22.5f || angle > 157.5f) return 0f;
				return value > 0f ? 1f : -1f;
			}
			if (snapAxis == AxisOptions.Vertical)
			{
				if (angle > 67.5f && angle < 112.5f) return 0f;
				return value > 0f ? 1f : -1f;
			}
			return value > 0f ? 1f : -1f;
		}
		if (value > 0f) return 1f;
		if (value < 0f) return -1f;
		return 0f;
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (!isPointerDown) return;
		if (eventData.pointerId != activePointerId) return;
		isPointerDown = false;
		activePointerId = -1;
		input = Vector2.zero;
		if (handle != null)
		{
			handle.anchoredPosition = Vector2.zero;
		}
	}

	protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
	{
		Vector2 localPoint = Vector2.zero;
		if (baseRect != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
		{
			return localPoint;
		}
		return Vector2.zero;
	}

	private UnityEngine.UI.Button _excavatorBtn;
	private Camera _excavatorCam;

	protected bool IsPointerOverMultiplierButton(Vector2 screenPos)
	{
		if (_excavatorBtn == null)
		{
			UnityEngine.UI.Button[] btns = Resources.FindObjectsOfTypeAll<UnityEngine.UI.Button>();
			for (int i = 0; i < btns.Length; i++)
			{
				if (btns[i].name == "Button" && btns[i].transform.parent != null && btns[i].transform.parent.name == "ExcavatorMultiplier")
				{
					_excavatorBtn = btns[i];
					break;
				}
			}
		}

		if (_excavatorBtn != null && _excavatorBtn.gameObject.activeInHierarchy && _excavatorBtn.interactable)
		{
			if (_excavatorCam == null) _excavatorCam = Camera.main;
			RectTransform rect = _excavatorBtn.GetComponent<RectTransform>();
			if (RectTransformUtility.RectangleContainsScreenPoint(rect, screenPos, _excavatorCam))
			{
				_excavatorBtn.onClick.Invoke();
				return true;
			}
		}
		return false;
	}
}
