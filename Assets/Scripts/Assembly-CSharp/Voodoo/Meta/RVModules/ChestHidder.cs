using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta.RVModules
{
	public class ChestHidder : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CWaitBeforeItemAnimator_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

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

		public TextMeshProUGUI text;

		public Image image;

		private const string _open = "Open";

		private const float _delayItemAnimator = 0.25f;

		public void Set(RewardInfo parameters)
		{
		}

		public void Open()
		{
		}

		public void OnOpened()
		{
		}

		[AsyncStateMachine(typeof(_003CWaitBeforeItemAnimator_003Ed__8))]
		private void WaitBeforeItemAnimator()
		{
		}
	}
}
