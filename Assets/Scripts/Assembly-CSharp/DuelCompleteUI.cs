using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuelCompleteUI : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public DuelCompleteUI _003C_003E4__this;

		public float playerTargetFill;

		public float opponentTargetFill;

		internal void _003CRewardWinner_003Eb__0()
		{
		}

		internal void _003CRewardWinner_003Eb__1()
		{
		}

		internal void _003CRewardWinner_003Eb__2()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CRewardWinner_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DuelCompleteUI _003C_003E4__this;

		private _003C_003Ec__DisplayClass28_0 _003C_003E8__1;

		private float _003Ctimer_003E5__2;

		private float _003Cdelta_003E5__3;

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
		public _003CRewardWinner_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CWaitForGamified_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DuelCompleteUI _003C_003E4__this;

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
		public _003CWaitForGamified_003Ed__29(int _003C_003E1__state)
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

	public Image PlayerBar;

	public Image OpponentBar;

	public Image OpponentFlag;

	public RectTransform BarParent;

	public RectTransform BarSeparator;

	public Text TitleText;

	public TextMeshProUGUI PlayerName;

	public TextMeshProUGUI PlayerCash;

	public TextMeshProUGUI OpponentName;

	public TextMeshProUGUI OpponentCash;

	public TextMeshProUGUI MainCash;

	private float _playerMoney;

	private float _opponentMoney;

	private float _totalMoney;

	public GameObject WinningObject;

	public Button NextFightButton;

	public Button ReturnToMain;

	public GamifiedRVView RVView;

	public TextMeshProUGUI MultipliedCash;

	public GameObject RewardFX;

	private CanvasGroup _canvasGroup;

	private void DisplayFX()
	{
	}

	private void Start()
	{
	}

	private void RestartDuel()
	{
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	public void Fill(float playerMoney, float opponentMoney)
	{
	}

	private void SetupBar()
	{
	}

	[IteratorStateMachine(typeof(_003CRewardWinner_003Ed__28))]
	private IEnumerator RewardWinner()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CWaitForGamified_003Ed__29))]
	private IEnumerator WaitForGamified()
	{
		return null;
	}

	private void Update()
	{
	}
}
