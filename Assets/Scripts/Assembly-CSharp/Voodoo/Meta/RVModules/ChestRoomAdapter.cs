using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class ChestRoomAdapter : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCollectReward_003Ed__29 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public ChestRoomAdapter _003C_003E4__this;

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
		private struct _003CFadeIn_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public ChestRoomAdapter _003C_003E4__this;

			public float endValue;

			public float duration;

			private float _003CbaseAlpha_003E5__2;

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
		private struct _003CShowButtons_003Ed__22 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public ChestRoomAdapter _003C_003E4__this;

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
		private CanvasGroup canvasGroup;

		[SerializeField]
		private ChestRoomView chestRoomView;

		[SerializeField]
		private GameObject rvButton;

		[SerializeField]
		private GameObject noThanksButton;

		[SerializeField]
		private List<RewardInfo> defaultRewards;

		[SerializeField]
		private RewardInfo defaultBestReward;

		[SerializeField]
		private AudioClip bigGemSound;

		private RewardInfo _bestReward;

		private Action _callback;

		private List<RewardInfo> _rewardsLeft;

		private CancellationTokenSource _cancellationTokenSource;

		private static ChestRoomAdapter _instance;

		public RewardInfo BestReward => null;

		public static ChestRoomAdapter Instance => null;

		private void Awake()
		{
			HideCanvasGroup();
		}

		private void HideCanvasGroup()
		{
			if (canvasGroup != null)
			{
				canvasGroup.alpha = 0f;
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
			}
			gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
		}

		private void InitRewards()
		{
		}

		public void Show(Action onClose)
		{
		}

		[AsyncStateMachine(typeof(_003CFadeIn_003Ed__21))]
		private void FadeIn(float endValue, float duration)
		{
		}

		[AsyncStateMachine(typeof(_003CShowButtons_003Ed__22))]
		public Task ShowButtons()
		{
			return null;
		}

		public void HideButtons()
		{
		}

		public void Hide()
		{
		}

		public void OnNoThanksClicked()
		{
		}

		public void OnRVClicked()
		{
		}

		private void OnComplete(bool success)
		{
		}

		public RewardInfo NextReward()
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CCollectReward_003Ed__29))]
		public Task CollectReward()
		{
			return null;
		}

		private void AddCurrency(RewardInfo reward)
		{
		}

		public static bool IsInAB()
		{
			return false;
		}

		public static void DoVibrateMedium()
		{
		}

		public static int GetCurrentLevel()
		{
			return 0;
		}

		public static void PlaySound(AudioClip clip)
		{
		}
	}
}
