using System;
using UnityEngine;
using Voodoo.Pattern;

public class CoinFxEmitter : SingletonMB<CoinFxEmitter>
{
	[SerializeField]
	private MovingFx m_CoinUIPrefab;

	private GameObject m_CurrentCoinContainer;

	public void EmitFxToward(CoinFxReceiver _Receiver, Vector2 _AnchoredEmitPosition, Action OnCompleted = null)
	{
	}
}
