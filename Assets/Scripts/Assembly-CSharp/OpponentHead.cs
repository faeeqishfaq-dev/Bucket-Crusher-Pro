using UnityEngine;

public class OpponentHead : MonoBehaviour
{
	[SerializeField]
	private Rigidbody[] _rigs;

	private Progress progress;

	private GameplayAudio gameplayAudio;

	private GameplayHaptic gameplayHaptic;

	private float rotationSpeed;

	public int amount;

	private MapCreatorEditor _mapGenerator;

	private void Start()
	{
	}

	public static void AddPhysics(Collider other)
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	public void SetKinematic(bool active)
	{
	}

	public void UpdateRotationSpeed()
	{
	}
}
