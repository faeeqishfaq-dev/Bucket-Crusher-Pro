using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PowerTimer : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSpendTime_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PowerTimer _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CSpendTime_003Ed__9(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private float timer;

	[SerializeField]
	private TextMeshPro _t;

	[SerializeField]
	private ParticleSystem _start;

	[SerializeField]
	private ParticleSystem _finish;

	public Transform _head;

	private Upgrade _u;

	private void Start()
	{
	}

	public void StartTimer(float t)
	{
	}

	public void FinishPower()
	{
	}

	[IteratorStateMachine(typeof(_003CSpendTime_003Ed__9))]
	private IEnumerator SpendTime()
	{
		return null;
	}
}
