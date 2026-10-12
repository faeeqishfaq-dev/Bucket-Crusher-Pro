using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Voodoo.Pattern;

public class ExcavatorBoosterController : SingletonMB<ExcavatorBoosterController>
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public int progress;

		internal bool _003CCheckForButton_003Eb__0()
		{
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _003CCheckEco_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ExcavatorBoosterController _003C_003E4__this;

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
		public _003CCheckEco_003Ed__14(int _003C_003E1__state)
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
	private sealed class _003CCheckForButton_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ExcavatorBoosterController _003C_003E4__this;

		private _003C_003Ec__DisplayClass16_0 _003C_003E8__1;

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
		public _003CCheckForButton_003Ed__16(int _003C_003E1__state)
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

	public List<BoostButtonSettings> ButtonSettings;

	public MissileLauncher Launcher;

	public GameObject BoostButton;

	public GameObject VideoImage;

	public TextMeshProUGUI BoostDescription;

	public Image icon;

	private BoostButtonType CurrentBoost;

	[Header("Eco Mode")]
	//public DOTweenAnimation _solarPanel;

	private static bool FirstUse
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private void Start()
	{
		if (VideoImage != null) VideoImage.SetActive(false);
	}

	private void OnGameEnd()
	{
	}

	private void OnGameStart()
	{
	}

	[IteratorStateMachine(typeof(_003CCheckEco_003Ed__14))]
	private IEnumerator CheckEco()
	{
		return null;
	}

	private bool IsInterstitialAvailable()
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CCheckForButton_003Ed__16))]
	private IEnumerator CheckForButton()
	{
		return null;
	}

	private void SetBooster(BoostButtonType current)
	{
	}

	public void OnClick()
	{
		AwardBooster(false);
	}

	private void AwardBooster(bool interstitialShown)
	{
	}
}
