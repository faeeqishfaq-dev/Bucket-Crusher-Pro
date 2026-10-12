using UnityEngine;

public class OpponentControl : MonoBehaviour
{
	[SerializeField]
	private Rigidbody[] _hands;

	[SerializeField]
	private MoveDirection _joy;

	private Rigidbody _head;

	[SerializeField]
	private float _speedKf;

	[SerializeField]
	private Vector2 MaxBounds;

	[SerializeField]
	private Vector2 MinBounds;

	private Vector3 currentTarget;

	private float lastDecision;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetHead(Rigidbody h)
	{
	}

	private void FixedUpdate()
	{
	}

	internal void ReplaceJoystick(MoveDirection joystick)
	{
	}
}
