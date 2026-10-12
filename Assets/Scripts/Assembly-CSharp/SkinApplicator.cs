using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class SkinApplicator : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CStartDropAnimation_003Ed__10 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public SkinApplicator _003C_003E4__this;

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

	public bool IsOpponent;

	public Transform HeadParent;

	public GameObject HeadObject;

	public Transform[] HeadParents;

	public List<GameObject> HeadObjects;

	public List<MeshRenderer> Cylinders;

	public List<MeshRenderer> OutsideJoints;

	public List<MeshRenderer> InsideJoints;

	public Transform DrillJoint;

	private void Start()
	{
	}

	[AsyncStateMachine(typeof(_003CStartDropAnimation_003Ed__10))]
	private void StartDropAnimation()
	{
	}

	private void OnDestroy()
	{
	}

	public void ApplySkin()
	{
	}

	private Item GetSelectedSkinItem()
	{
		return null;
	}

	private void ApplySkinAttributes(Item item)
	{
	}

	private void InstantiateHeadObject(Item item, bool drillMode)
	{
	}

	private void HandleNoDrillMode(Item item)
	{
	}

	private void UpdateMeshes(Item item)
	{
	}

	private void HandleDrillMode()
	{
	}
}
