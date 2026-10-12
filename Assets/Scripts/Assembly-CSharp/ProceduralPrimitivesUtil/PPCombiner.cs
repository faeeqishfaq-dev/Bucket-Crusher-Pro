using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class PPCombiner : PPBase
	{
		public List<GameObject> elements;

		public bool includeChildren;

		public bool applyTargetsTransform;

		private List<Mesh> mList;

		protected override void CreateMesh()
		{
		}

		private void AddMesh(Mesh m, Transform trans = null)
		{
		}
	}
}
