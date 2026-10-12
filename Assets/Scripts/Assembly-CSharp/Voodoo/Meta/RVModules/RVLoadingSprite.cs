using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta.RVModules
{
	public class RVLoadingSprite : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPrepareCheckRVAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public RVLoadingSprite _003C_003E4__this;

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
		private struct _003CRotate_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Vector3 rotation;

			public float duration;

			public RVLoadingSprite _003C_003E4__this;

			private float _003CtimeElapsed_003E5__2;

			private Vector3 _003CcurrentRotation_003E5__3;

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
		private struct _003CRotateInfinite_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public RVLoadingSprite _003C_003E4__this;

			public Vector3 rotation;

			public float duration;

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

		public Sprite loadingSprite;

		public Image defaultImage;

		public Button buttonRV;

		public bool handleInteractivity;

		private Sprite _defaultSprite;

		private Color _defaultColor;

		private bool _subscribed;

		private CancellationTokenSource _cancellationTokenSource;

		private void Start()
		{
		}

		private void OnEnable()
		{
		}

		private void PrepareCheckRV()
		{
		}

		[AsyncStateMachine(typeof(_003CPrepareCheckRVAsync_003Ed__11))]
		private Task PrepareCheckRVAsync()
		{
			return null;
		}

		private void DoCheckRV()
		{
		}

		[AsyncStateMachine(typeof(_003CRotateInfinite_003Ed__13))]
		private Task RotateInfinite(Vector3 rotation, float duration)
		{
			return null;
		}

		[AsyncStateMachine(typeof(_003CRotate_003Ed__14))]
		private Task Rotate(Vector3 rotation, float duration)
		{
			return null;
		}

		private void OnDisable()
		{
		}

		private void OnRVReady(bool success)
		{
		}
	}
}
