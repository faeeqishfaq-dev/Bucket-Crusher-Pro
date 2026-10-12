using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;
using Voodoo.Pattern;

public class RvDrillBoost : SingletonMB<RvDrillBoost>
{
	[CompilerGenerated]
	private sealed class _003CCheckForButton_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RvDrillBoost _003C_003E4__this;

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
		public _003CCheckForButton_003Ed__14(int _003C_003E1__state)
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
	private sealed class _003CDrillBoostTimer_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RvDrillBoost _003C_003E4__this;

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
		public _003CDrillBoostTimer_003Ed__17(int _003C_003E1__state)
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
	private GameObject _button;

	[SerializeField]
	private GameObject _adsPlayImage;

	[SerializeField]
	private GameObject _buttonText;

	[SerializeField]
	private UIShiny _UIShiny;

	[SerializeField]
	private Text _timer;

	private List<TreadmillController> treadmills;

	public bool bonusActive;

	private int currentTime;

	public bool bonusUsed => false;

	private void Awake()
	{
		if (_button != null) _button.SetActive(false);
		gameObject.SetActive(false);
	}

	private void Start()
	{
		if (_adsPlayImage != null) _adsPlayImage.SetActive(false);
	}

	private void OnGameStart()
	{
	}

	private void OnGameEnd()
	{
	}

	private bool IsInterstitialAvailable()
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CCheckForButton_003Ed__14))]
	private IEnumerator CheckForButton()
	{
		return null;
	}

	public void ButtonCliced()
	{
		ActiveBonus();
	}

	private void ActiveBonus()
	{
		bonusActive = true;
	}

	[IteratorStateMachine(typeof(_003CDrillBoostTimer_003Ed__17))]
	private IEnumerator DrillBoostTimer()
	{
		return null;
	}
}
