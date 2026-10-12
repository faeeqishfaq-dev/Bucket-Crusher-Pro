using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Voodoo.Utils.Ease;

namespace Voodoo.Meta
{
	public class LeaderboardLayoutAnimation
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CAnimationAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardLayoutAnimation _003C_003E4__this;

			public CancellationToken cancelToken;

			private TaskAwaiter _003C_003Eu__1;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__2;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CIdleAnimationAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken cancelToken;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CLaunchAnimationAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardLayoutAnimation _003C_003E4__this;

			public CancellationToken cancelToken;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CMoveAnchoredPositionAsync_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardPlayerSlot slot;

			public Vector2 finalAnchoredPosition;

			public float duration;

			public CancellationToken cancelToken;

			private Vector2 _003CinitialAnchoredPosition_003E5__2;

			private float _003CtimeElapsed_003E5__3;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CProcessSlotsAndScoreIncreaseAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardLayoutAnimation _003C_003E4__this;

			public CancellationToken cancelToken;

			private float _003Cduration_003E5__2;

			private LeaderboardRankLogic _003CrankLogic_003E5__3;

			private int _003CrankStep_003E5__4;

			private IReadOnlyList<LeaderboardPlayerSlot> _003Cslots_003E5__5;

			private CalculationEasing.Function _003CeasingFunction_003E5__6;

			private float _003CelapsedTime_003E5__7;

			private float _003CcurrentEaseDuration_003E5__8;

			private int _003CdirectionMultiplier_003E5__9;

			private int _003Ci_003E5__10;

			private float _003CcurrentTime_003E5__11;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CScalePlayerSlotAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LeaderboardLayoutAnimation _003C_003E4__this;

			public Vector3 finalScale;

			public float duration;

			public CancellationToken cancelToken;

			private Vector3 _003CinitialScale_003E5__2;

			private float _003CtimeElapsed_003E5__3;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

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

		private const int _firstPlaceDuration = 1500;

		private const int _delayBetweenAnimPhase = 300;

		private const float _rankStepPower = 1.3f;

		private const Ease _easeType = Ease.EaseOutQuad;

		private readonly LeaderboardLayoutUI _layoutUI;

		private readonly LeaderboardAnimationData _leaderboardAnimationData;

		private readonly float _margin;

		private readonly LeaderboardPlayerSlot _playerSlot;

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

		public LeaderboardLayoutAnimation(LeaderboardLayoutUI layoutUI, LeaderboardAnimationData leaderboardAnimationData, float margin)
		{
		}

		[AsyncStateMachine(typeof(_003CLaunchAnimationAsync_003Ed__15))]
		public Task LaunchAnimationAsync(CancellationToken cancelToken = default(CancellationToken))
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CIdleAnimationAsync_003Ed__16))]
		private Task IdleAnimationAsync(CancellationToken cancelToken)
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CAnimationAsync_003Ed__17))]
		private Task AnimationAsync(CancellationToken cancelToken = default(CancellationToken))
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CMoveAnchoredPositionAsync_003Ed__18))]
		private Task MoveAnchoredPositionAsync(LeaderboardPlayerSlot slot, Vector2 finalAnchoredPosition, float duration, CancellationToken cancelToken = default(CancellationToken))
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CScalePlayerSlotAsync_003Ed__19))]
		private Task ScalePlayerSlotAsync(Vector3 finalScale, float duration, CancellationToken cancelToken = default(CancellationToken))
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CProcessSlotsAndScoreIncreaseAsync_003Ed__20))]
		private Task ProcessSlotsAndScoreIncreaseAsync(CancellationToken cancelToken = default(CancellationToken))
		{
			return null;
		}
	}
}
