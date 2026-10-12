using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Voodoo.Pattern;

public class TransitionAnimationController : SingletonMB<TransitionAnimationController>
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public TransitionAnimationController _003C_003E4__this;

		public Camera mainCam;

		internal void _003CStartTransition_003Eb__0()
		{
		}
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CCameraFollow_003Ed__17 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public Camera mainCam;

		private float _003Ctimer_003E5__2;

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
	private struct _003CStartTransition_003Ed__16 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public TransitionAnimationController _003C_003E4__this;

		private _003C_003Ec__DisplayClass16_0 _003C_003E8__1;

		private float _003Cscale_003E5__2;

		private float _003Ctimer_003E5__3;

		private float _003CendSize_003E5__4;

		private Vector3 _003CcamStartPosition_003E5__5;

		private Vector3 _003CcamEndPosition_003E5__6;

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
	private struct _003CWatchCoins_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public TransitionAnimationController _003C_003E4__this;

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

	public Vector3 transitionEndPosition;

	public Vector3 transitionStartPosition;

	public Transform CameraPosition;

	public Transform CameraStartPosition;

	public Transform CameraEndPosition;

	public Transform startPosition;

	public Transform endPosition;

	public Transform headParent;

	private GameObject headVisual;

	public Animator lockAnim;

	public TransitionMachine Machine;

	public GameObject CoinExplosionEffect;

	public GameObject TrailEffect;

	public Transform tubeObject;

	public GameObject gameCompleteCanvas;

	private void Awake()
	{
	}

	[AsyncStateMachine(typeof(_003CStartTransition_003Ed__16))]
	public void StartTransition()
	{
	}

	[AsyncStateMachine(typeof(_003CCameraFollow_003Ed__17))]
	private void CameraFollow(Camera mainCam)
	{
	}

	[AsyncStateMachine(typeof(_003CWatchCoins_003Ed__18))]
	private void WatchCoins()
	{
	}

	private void Update()
	{
	}
}
