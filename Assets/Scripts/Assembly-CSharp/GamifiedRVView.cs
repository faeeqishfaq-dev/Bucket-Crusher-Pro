using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class GamifiedRVView : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CMoveCursor_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GamifiedRVView _003C_003E4__this;

		private int _003Cdirection_003E5__2;

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
		public _003CMoveCursor_003Ed__4(int _003C_003E1__state)
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

	public RectTransform Cursor;

	private Sequence sequence;

	public Action<int> OnClicked;

	public int GetValue()
	{
		return 0;
	}

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CMoveCursor_003Ed__4))]
	private IEnumerator MoveCursor()
	{
		return null;
	}

	private void OnEnable()
	{
	}

	public void OnRvClick()
	{
		OnClicked?.Invoke(GetValue());
	}

	public void OnNoThanks()
	{
	}

	private void Update()
	{
	}
}
