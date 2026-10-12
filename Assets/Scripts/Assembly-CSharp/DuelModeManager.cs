using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Voodoo.Meta;
using Voodoo.Pattern;
using Voodoo.Pattern.Random;

public class DuelModeManager : SingletonMB<DuelModeManager>
{
	[CompilerGenerated]
	private sealed class _003CResetContainer_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DuelModeManager _003C_003E4__this;

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
		public _003CResetContainer_003Ed__37(int _003C_003E1__state)
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
	private sealed class _003CWaitAndEnableOpponent_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CWaitAndEnableOpponent_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CWaitAndEnablePlayer_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CWaitAndEnablePlayer_003Ed__30(int _003C_003E1__state)
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

	public Transform DuelSpawnPosition;

	public Transform DuelCameraPosition;

	public DuelUpgradeLevels PlayerUpgrades;

	public DuelUpgradeLevels OpponentUpgrades;

	public List<DuelUpgrade> RVUpgrades;

	public List<DuelUpgrade> RareUpgrades;

	public List<DuelUpgrade> NormalUpgrades;

	public DuelCompleteUI DuelCompleteUI;

	public DuelPlayerNames PlayerNames;

	public UpgradeSelectionArea UpgradesContainer;

	public bool DuelStarted;

	public string OpponentSkin;

	public FlagPool _flags;

	public Sprite _opponentSprite;

	public Leaderboard _leaderboard;

	public Action OnDuelStarted;

	public string OpponentName;

	private float popupTimer;

	public Action OnUpgradedPlayer;

	public Action OnUpgradedOpponent;

	public float PlayerMoney;

	public float OpponentMoney;

	public string PlayersName
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void ChoseFlag()
	{
	}

	public void SetUpCompetitors()
	{
	}

	public void SelectUpgrade(bool isPlayer, DuelUpgrade upgrade)
	{
	}

	[IteratorStateMachine(typeof(_003CWaitAndEnableOpponent_003Ed__28))]
	private IEnumerator WaitAndEnableOpponent()
	{
		return null;
	}

	internal void NameUpdated()
	{
	}

	[IteratorStateMachine(typeof(_003CWaitAndEnablePlayer_003Ed__30))]
	private IEnumerator WaitAndEnablePlayer()
	{
		return null;
	}

	private void Update()
	{
	}

	public void SavePlayerCash(float value)
	{
	}

	public float GetPlayerCash()
	{
		return 0f;
	}

	public void ShowLeaderboardTheMain()
	{
	}

	public void ShowLeaderboard(Action onComplete)
	{
	}

	public void HideLeaderboard()
	{
	}

	[IteratorStateMachine(typeof(_003CResetContainer_003Ed__37))]
	private IEnumerator ResetContainer()
	{
		return null;
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void Start()
	{
	}

	private void FillInStartUpgrades()
	{
	}

	public void OnGameFinished()
	{
	}

	private void GameStarted()
	{
	}

	public void FillInNormalUpgrades()
	{
	}
}
