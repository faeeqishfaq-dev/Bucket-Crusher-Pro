using UnityEngine;

public class DroneHandler : MonoBehaviour
{
	public DroneHead Head;

	public Rigidbody Body;

	public float ChangeTargetDist;

	public float ChangeTargetTime;

	public Vector3 InitialPos;

	public Vector3 InitialRot;

	public float Strength;

	public GameObject DepleteFX;

	public GameObject FuelDock;

	public Transform FillAmount;

	private float _drillTime;

	private float _targetTimer;

	private Vector3 _target;

	private MapCreatorEditor _mapCreator;

	private float _initialTime;

	public bool IsDrilling()
	{
		return false;
	}

	public void OnGameStarted(int duration)
	{
	}

	public void OnRVTaken()
	{
	}

	public void StartDrilling()
	{
	}

	public void StopDrilling(bool instant = true)
	{
	}

	public void DoReset()
	{
	}

	private void FindNewTarget()
	{
	}

	private void FixedUpdate()
	{
	}
}
