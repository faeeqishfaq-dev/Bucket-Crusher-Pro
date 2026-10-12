using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoosterSpawn : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CSwichCollider_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BoosterSpawn _003C_003E4__this;

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
		public _003CSwichCollider_003Ed__7(int _003C_003E1__state)
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

	[SerializeField]
	private float[] _radiouses;

	[SerializeField]
	private GameObject _boosterBoj;

	[SerializeField]
	private List<Transform> points;

	private List<Transform> avaliblePoints;

	private List<GameObject> _currentBoosters;

	public void Refrash()
	{
	}

	private void OnTriggerStay(Collider other)
	{
	}

	[IteratorStateMachine(typeof(_003CSwichCollider_003Ed__7))]
	private IEnumerator SwichCollider()
	{
		return null;
	}
}
