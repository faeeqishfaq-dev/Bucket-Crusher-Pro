using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Voodoo.Utils
{
	public class Delay : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CTimer_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Delay _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CTimer_003Ed__40(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public Action<float> OnDelayRunning;

		public Action OnDelayCompleted;

		public Action OnDelaySkipped;

		public Action OnDelayCancelled;

		private Func<bool> _skipAction;

		private Func<bool> _cancelAction;

		protected float _delay;

		private float _startTime;

		private float _elapsedTime;

		private IEnumerator _timer;

		private TimeScale _scaleTime;

		private DelayState _state;

		public DelayState State
		{
			get
			{
				return default(DelayState);
			}
			private set
			{
			}
		}

		private float CurrentTime => 0f;

		private float Interval { get; set; }

		private float ElapsedTime => 0f;

		private float TotalElapsedTime => 0f;

		private float RemainingTime => 0f;

		public Delay Timescale(TimeScale timeScale)
		{
			return null;
		}

		public Delay Frequency(float frequency)
		{
			return null;
		}

		public Delay SetDelay(float delay)
		{
			return null;
		}

		public Delay SkipIf(Func<bool> predicate)
		{
			return null;
		}

		public Delay CancelIf(Func<bool> predicate)
		{
			return null;
		}

		[ContextMenu("Begin")]
		public Delay Begin()
		{
			return null;
		}

		[ContextMenu("Skip")]
		public Delay Skip()
		{
			return null;
		}

		[ContextMenu("Cancel")]
		public void Cancel()
		{
		}

		[ContextMenu("Pause")]
		public Delay Pause()
		{
			return null;
		}

		[ContextMenu("Resume")]
		public Delay Resume()
		{
			return null;
		}

		public void End()
		{
		}

		private void Update()
		{
		}

		private void SetStartTime()
		{
		}

		[IteratorStateMachine(typeof(_003CTimer_003Ed__40))]
		private IEnumerator Timer()
		{
			return null;
		}

		private void DisplayDelayerName(DelayState state)
		{
		}

		private void OnStateChanged(DelayState state)
		{
		}

		protected virtual void OnTimeOut()
		{
		}
	}
}
