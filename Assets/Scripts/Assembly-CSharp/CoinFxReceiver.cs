using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CoinFxReceiver : MonoBehaviour
{
	public RectTransform m_RectTransform;

	[ContextMenu("Request Coins")]
	public void RequestCoins(Action _OnCoinCollected = null)
	{
	}
}
