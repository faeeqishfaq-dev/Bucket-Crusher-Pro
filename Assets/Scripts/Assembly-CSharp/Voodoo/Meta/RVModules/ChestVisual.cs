using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class ChestVisual : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CIdleLoop_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public ChestVisual _003C_003E4__this;

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

		public Animator animator;

		public float timeBetweenLoop;

		private static readonly int reset;

		private static readonly int open;

		private static readonly int giggle;

		private bool isAlive;

		public void SetUp(float delay)
		{
		}

		[AsyncStateMachine(typeof(_003CIdleLoop_003Ed__7))]
		private void IdleLoop(float delay)
		{
		}

		public void Open()
		{
		}

		public void Clean()
		{
		}
	}
}
