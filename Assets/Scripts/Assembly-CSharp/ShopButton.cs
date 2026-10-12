using System;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
	public ShopButtonState CurrentState;

	public GameObject Notification;

	public Action OnClicked;

	public Action AnimationComplete;

	public Action AnimationStarted;

	public Vector2 ShowingSize;

	public Vector2 HiddinSize;

	public RectTransform thisRect;

	public void SwitchState(ShopButtonState newState)
	{
	}

	public virtual void SwitchNotification(bool itemAvailable)
	{
	}

	public void AnimationCompleted()
	{
	}

	public virtual void OnCLick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
