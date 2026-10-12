using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class ChestRoomView : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003COnChestOpened_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public ChestRoomView _003C_003E4__this;

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

		private Chest currentChest;

		[Space]
		public ChestArea chestArea;

		public KeyArea keyArea;

		public BestRewardArea bestRewardArea;

		public AudioClip chestOpenSound;

		public int keysMax;

		public int chestsMax;

		private bool _lastChestHasOpened;

		private CancellationTokenSource _cancellationTokenSource;

		public Transform Root => null;

		public void FillUpKeys()
		{
		}

		public void UseAKey(Chest chest)
		{
		}

		public void OpenChest(Chest chest)
		{
		}

		public void OnOpen()
		{
		}

		[AsyncStateMachine(typeof(_003COnChestOpened_003Ed__15))]
		public Task OnChestOpened()
		{
			return null;
		}

		protected void PlayCollect()
		{
		}

		protected void OnDestroy()
		{
		}
	}
}
