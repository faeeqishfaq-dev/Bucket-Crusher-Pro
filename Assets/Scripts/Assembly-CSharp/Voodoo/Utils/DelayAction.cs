using System;
using UnityEngine.Events;

namespace Voodoo.Utils
{
	public class DelayAction : Delay
	{
		public Action OnActionStart;

		public Action OnActionCompleted;

		private Action m_Action;

		public DelayAction SetAction(Action action)
		{
			return null;
		}

		public DelayAction SetAction(UnityAction action)
		{
			return null;
		}

		public new DelayAction SetDelay(float delay)
		{
			return null;
		}

		protected override void OnTimeOut()
		{
		}
	}
}
