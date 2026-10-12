using UnityEngine;

public class DroneHead : MonoBehaviour
{
	public GameObject DrillFX;

	private Progress progress;

	private GameplayAudio gameplayAudio;

	private GameplayHaptic gameplayHaptic;

	private float rotationSpeed;

	public int amount;

	private MapCreatorEditor _mapGenerator;

	private float _drillEnableTimer;

	private void Start()
	{
	}

	public static void AddPhysics(Collider other)
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
	}
}
