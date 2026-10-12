using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
	public GameObject BoostRV;

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void OnClick_BoostGame()
	{
		if (GameManager.Instance != null)
		{
			GameManager.Instance.StartGame();
		}
	}

	private void Update()
	{
	}
}
