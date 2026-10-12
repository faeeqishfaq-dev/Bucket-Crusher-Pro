using System;
using UnityEngine.Events;

namespace Voodoo.Utils
{
	public static class ExtendedAction
	{
		public static DelayAction Delay(this Action action, float delay)
		{
			return null;
		}

		public static DelayAction Delay(this UnityAction action, float delay)
		{
			return null;
		}
	}
}
