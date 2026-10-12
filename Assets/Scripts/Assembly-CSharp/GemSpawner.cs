using System;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject _brick;

	[SerializeField]
	private GameObject _key;

	private List<GameObject> _bricksPool;

	private GameObject key;

	private DateTime lastSpawnTime
	{
		get
		{
			return default(DateTime);
		}
		set
		{
		}
	}

	private void OnDestroy()
	{
	}

	public void SpawnKey(List<Transform> bricks)
	{
	}

	public void SpawnGems(List<Transform> bricks)
	{
	}

	private void SpawnKey()
	{
	}

	private GameObject Brick()
	{
		return null;
	}
}
