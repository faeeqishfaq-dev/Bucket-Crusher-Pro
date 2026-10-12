using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoningManager : MonoBehaviour
{
	[Serializable]
	public class ZoningDayConfig
	{
		public Transform SkyPrefab;

		public Transform GroundPrefab;
	}

	[Serializable]
	public class ZoningCycleConfig
	{
		public List<ZoningDayConfig> DayConfigs;
	}

	public const string ZONING_PREF = "VoodooZoningIndex";

	public List<ZoningCycleConfig> Configs;

	public List<GameObject> ControlElements;

	private Transform _currentSky;

	private Transform _currentGround;

	private int lastLoaded;

	public int RoundIndex
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void OnGameFinished()
	{
	}

	public void Refresh()
	{
	}
}
