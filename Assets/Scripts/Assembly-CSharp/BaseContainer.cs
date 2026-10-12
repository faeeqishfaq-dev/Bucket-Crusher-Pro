using System;
using System.Collections.Generic;
using UnityEngine;
using Voodoo.Pattern;

public class BaseContainer<T> : SingletonMB<T>
{
	public List<Item> DisplayItems;

	public Transform content;

	public BuyButton buttonPrefab;

	public Action OnCloseClicked;

	public List<BuyButton> buyButtons;

	public void OnCloseClick()
	{
	}

	public virtual void Fill()
	{
	}

	public void Hide()
	{
	}

	public void Show()
	{
	}

	public virtual void Refresh()
	{
	}

	public virtual void OnClicked()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
