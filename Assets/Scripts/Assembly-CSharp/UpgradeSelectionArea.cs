using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class UpgradeSelectionArea : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CWaitAndChose_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpgradeSelectionArea _003C_003E4__this;

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
		public _003CWaitAndChose_003Ed__11(int _003C_003E1__state)
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

	public List<UpgradeOption> Options;

	public RectTransform Visuals;

	public GameObject TimerParent;

	public TextMeshProUGUI timerText;

	public GameObject[] StartParents;

	public GameObject UpgradeReminder;

	public List<UpgradeOption> RVOptions;

	private List<UpgradeOption> ShownItems;

	internal void Fill(List<DuelUpgrade> rvUpgrades, List<DuelUpgrade> duelUpgrades, bool showReminder)
	{
	}

	internal void Hide()
	{
	}

	internal void Show()
	{
	}

	[IteratorStateMachine(typeof(_003CWaitAndChose_003Ed__11))]
	private IEnumerator WaitAndChose()
	{
		return null;
	}

	internal void SetTimer(float popupTimer)
	{
	}
}
