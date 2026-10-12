using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBonusManager : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CBubbleMove_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BubbleBonusManager _003C_003E4__this;

		private RectTransform _003Cpoint_003E5__2;

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
		public _003CBubbleMove_003Ed__21(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CChangeContent_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BubbleBonusManager _003C_003E4__this;

		private int _003CcontentId_003E5__2;

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
		public _003CChangeContent_003Ed__20(int _003C_003E1__state)
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
	private GameObject _bubbleObject;

	private RectTransform _bubbleObjectTransform;

	[SerializeField]
	private GameObject _bonusPanel;

	[SerializeField]
	private GameObject _doubleMoneyPanel;

	[SerializeField]
	private GameObject _oilPanel;

	[SerializeField]
	private Text _oilKfText;

	[SerializeField]
	private GameObject _longUpgradePanel;

	[SerializeField]
	private GameObject _oilUpgradePanel;

	[SerializeField]
	private GameObject _powerUpgradePanel;

	[SerializeField]
	private GameObject _sizeUpgradePanel;

	[SerializeField]
	private GameObject _bonusHolder;

	[SerializeField]
	private RectTransform[] _movePoints;

	private Animator bonusAnimator;

	public float fuelMultiplicator;

	private List<float> fuelMultiplicators;

	private void Awake()
	{
		DisableBubble();
	}

	private void Start()
	{
		DisableBubble();
	}

	public void BubbleClicked()
	{
	}

	public void ShowBubble()
	{
	}

	public void DisableBubble()
	{
		if (_bubbleObject != null) _bubbleObject.SetActive(false);
		if (_bonusPanel != null) _bonusPanel.SetActive(false);
		gameObject.SetActive(false);
	}

	private void ActiveBonus()
	{
	}

	[IteratorStateMachine(typeof(_003CChangeContent_003Ed__20))]
	private IEnumerator ChangeContent()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CBubbleMove_003Ed__21))]
	private IEnumerator BubbleMove()
	{
		return null;
	}
}
