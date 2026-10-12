using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Voodoo.Meta
{
	public class Leaderboard : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetInteractionCanvasActiveAsync_003Ed__33 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Leaderboard _003C_003E4__this;

			public float delay;

			public bool isActive;

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
		private struct _003CShowAsync_003Ed__30 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Leaderboard _003C_003E4__this;

			public Action onComplete;

			public int previousScore;

			public int newScore;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

			private TaskAwaiter _003C_003Eu__2;

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

		[Header("Project Reference")]
		[SerializeField]
		private LeaderboardConfigData _config;

		[Header("Hierarchy References")]
		[SerializeField]
		private Canvas _canvas;

		[SerializeField]
		private Canvas _interactionCanvas;

		[SerializeField]
		private LeaderboardLayout _layout;

		private LeaderboardAudio _audio;

		private LeaderboardHaptic _haptic;

		private CancellationTokenSource _interactionCanvasCancelSource;

		private Action _leaderboardEnded;

		private int _rankVibrationThreshold;

		private CancellationTokenSource _vibrationCancelSource;

		public static Leaderboard First { get; private set; }

		public bool IsVisible => false;

		public bool IsInteractionCanvasVisible => false;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void SetPlayerInfo(string playerName, Sprite playerFlag)
		{
		}

		public void Show(int previousScore, int newScore, Action onComplete = null)
		{
		}

		public void Show(int previousScore, int newScore, string playerName, Sprite playerFlag = null, Action onComplete = null)
		{
		}

		public void Next()
		{
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

		public void Stop()
		{
		}

		public void Restart(int previousScore, int newScore)
		{
		}

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__30))]
		private Task ShowAsync(int previousScore, int newScore, Action onComplete = null)
		{
			return null;
		}

		private void ShowInteractionCanvas()
		{
		}

		private void SetCanvasVisibility(Canvas canvas, bool isVisible)
		{
		}

		[AsyncStateMachine(typeof(_003CSetInteractionCanvasActiveAsync_003Ed__33))]
		private Task SetInteractionCanvasActiveAsync(bool isActive, float delay)
		{
			return null;
		}

		private void SetInteractionCanvasActive(bool isActive, float delay)
		{
		}

		private void LeaderboardShowFeedback()
		{
		}

		private void LeaderboardHideFeedback()
		{
		}

		private void PlayerRankFeedback()
		{
		}

		private void PlayerRankProgressionFeedback(int rank)
		{
		}

		private void OnRankUpdated()
		{
		}

		private void OnRankProgressionProcessed(int rank)
		{
		}
	}
}
