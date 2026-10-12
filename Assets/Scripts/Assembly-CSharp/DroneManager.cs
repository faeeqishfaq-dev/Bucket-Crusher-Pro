using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
	public Item DroneItem;

	public DroneHandler DefaultSkin;

	private List<DroneHandler> _currentDrone;

	private static DroneManager _instance;

	public static DroneManager Instance => null;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnItemPurchased()
	{
	}

	public void OnGameStarted()
	{
	}

	public void OnRVTaken()
	{
	}

	public void OnGameFinished()
	{
	}

	public bool IsDrilling()
	{
		return false;
	}

	public void SetDroneSkin()
	{
	}
}
