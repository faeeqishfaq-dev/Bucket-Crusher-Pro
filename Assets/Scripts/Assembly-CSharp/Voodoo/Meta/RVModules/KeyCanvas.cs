using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class KeyCanvas : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDelayedHideRoot_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public KeyCanvas _003C_003E4__this;

			public float delay;

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

		public List<GameObject> disabledKeys;

		public List<GameObject> enableKeys;

		public GameObject root;

		private CancellationTokenSource _cancellationTokenSource;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void OnKeyCollected()
		{
		}

		[AsyncStateMachine(typeof(_003CDelayedHideRoot_003Ed__7))]
		private Task DelayedHideRoot(float delay)
		{
			return null;
		}

		private void HideRoot()
		{
		}
	}
}
