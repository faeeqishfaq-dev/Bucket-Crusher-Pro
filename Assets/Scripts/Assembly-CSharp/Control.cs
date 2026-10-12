using UnityEngine;
using Object = UnityEngine.Object;

public class Control : MonoBehaviour
{
	private MoveDirection _joy;

	private Rigidbody _head;

	[SerializeField]
	private float _speedKf = 3f;

	// Multiplier that compensates for longer arms having more drag.
	// Set by UpdateSpeedForLength() after each stick swap.
	private float _lengthSpeedMultiplier = 1f;

	private void Awake()
	{
		EnsureJoystick();
	}

	private void Start()
	{
		EnsureJoystick();
	}

	private void EnsureJoystick()
	{
		if (_joy == null)
		{
			_joy = Object.FindAnyObjectByType<DynamicJoystick>();
			if (_joy == null)
			{
				_joy = Object.FindAnyObjectByType<Joystick>();
			}
		}
	}

	public Rigidbody HeadRigidbody => _head;

	public void SetHead(Rigidbody h)
	{
		_head = h;
	}

	/// <summary>
	/// Recalculates the speed multiplier based on the actual rigidbodies in the
	/// instantiated stick.  Longer arms contain more high-drag segments (linearDamping ≥ 10),
	/// so the head velocity must increase proportionally to keep movement consistent.
	///
	/// Base reference (1.prefab / Level 0): 2 high-drag segments.
	/// Each additional segment adds roughly 40 linearDamping to the chain, so we scale
	/// the multiplier by (highDragCount / 2).  A minimum of 1 is enforced.
	/// </summary>
	public void UpdateSpeedForLength(GameObject stick)
	{
		if (stick == null)
		{
			_lengthSpeedMultiplier = 1f;
			return;
		}

		Rigidbody[] rbs = stick.GetComponentsInChildren<Rigidbody>();
		int highDragCount = 0;
		foreach (Rigidbody rb in rbs)
		{
			if (rb.linearDamping >= 10f)
			{
				highDragCount++;
			}
		}

		// Base arm (Level 0) has 2 high-drag segments.
		// Scale linearly so the effective velocity compensates for extra drag.
		const int baseHighDrag = 2;
		_lengthSpeedMultiplier = highDragCount > baseHighDrag
			? (float)highDragCount / baseHighDrag
			: 1f;

		Debug.Log($"[Control] UpdateSpeedForLength: {highDragCount} high-drag segments, multiplier={_lengthSpeedMultiplier:F2}, effective speed={_speedKf * _lengthSpeedMultiplier:F2}");
	}

	private void FixedUpdate()
	{
		if (_head == null) return;
		if (_joy == null)
		{
			EnsureJoystick();
			if (_joy == null)
			{
				_head.linearVelocity = Vector3.zero;
				return;
			}
		}

		Vector2 dir = _joy.Direction;
		if (dir.sqrMagnitude > 0.001f)
		{
			Vector3 move = new Vector3(dir.x, dir.y, 0f) * _speedKf * _lengthSpeedMultiplier;
			_head.linearVelocity = move;
		}
		else
		{
			_head.linearVelocity = Vector3.zero;
		}
	}

	internal void ReplaceJoystick(MoveDirection joystick)
	{
		_joy = joystick;
	}
}
