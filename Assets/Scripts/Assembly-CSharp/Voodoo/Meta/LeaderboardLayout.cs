using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Voodoo.Meta
{
	public class LeaderboardLayout : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowAsync_003Ed__32 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardLayout _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[SerializeField]
		private RectTransform _slotParent;

		[SerializeField]
		private LeaderboardData _leaderboardData;

		[SerializeField]
		public LeaderboardRankData _leaderboardRankData;

		[SerializeField]
		private LeaderboardAnimationData _leaderboardAnimationData;

		private LeaderboardAnalytics _analytics;

		private CancellationTokenSource _animationCancelSource;

		private LeaderboardLayoutAnimation _layoutAnimation;

		private LeaderboardLayoutUI _layoutUI;

		private LeaderboardPlayerInfo _playerInfo;

		private LeaderboardRankLogic _rankLogic;

		public RectTransform SlotParent => null;

		public LeaderboardData LeaderboardData => null;

		public bool IsAnimating { get; private set; }

		public event Action<int> rankProgressionProcessed
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event Action rankUpdated
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void SetPlayerInfo(string playerName, Sprite playerFlag)
		{
		}

		public int GetPlayerRank()
		{
			return 0;
		}

		public int GetNextRank(int previousScore, int newScore)
		{
			return 0;
		}

		public int GetNextRankFromStep(int previousScore, int newScore, int rankStep)
		{
			return 0;
		}

		public int GetRankEarned(int newScore)
		{
			return 0;
		}

		public void Initialize(int previousScore, int newScore)
		{
		}

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__32))]
		public Task ShowAsync()
		{
			return null;
		}

		public void Hide()
		{
		}

		private void OnRankProgressionProcessed(int rank)
		{
		}

		private void OnRankUpdated()
		{
		}
	}
}
