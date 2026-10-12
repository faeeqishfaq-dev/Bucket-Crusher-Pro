using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public static class UsefulExtensions
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CWaitSeconds_003Ed__5 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public float seconds;

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

	public static T GetRandom<T>(this List<T> list)
	{
		return default(T);
	}

	public static List<T> GetRandomRange<T>(this List<T> list, int count)
	{
		return null;
	}

	public static void Hide(this CanvasGroup _canvas)
	{
	}

	public static void Show(this CanvasGroup _canvas)
	{
	}

	public static void SetActive(this List<GameObject> list, bool isActive)
	{
	}

	[AsyncStateMachine(typeof(_003CWaitSeconds_003Ed__5))]
	public static Task WaitSeconds(float seconds)
	{
		return null;
	}

	public static List<T> RemoveEmpty<T>(this List<T> list)
	{
		return null;
	}

	public static void SetTagsRecursive(this GameObject parent, string tag)
	{
	}

	public static int Round(this int value, int factor)
	{
		return 0;
	}

	public static int Round(this int value)
	{
		return 0;
	}

	public static bool Choose(this float chance)
	{
		return false;
	}

	public static float Range(this float range, float baseRange)
	{
		return 0f;
	}

	public static float Range(this float range)
	{
		return 0f;
	}
}
