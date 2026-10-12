using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class DoubleSphere : PPBase
	{
		[Header("Basic parameters")]
		public float radius1;

		public float radius2;

		[Header("Segments")]
		public int segments;

		[Header("Slice")]
		public bool sliceOn;

		public float sliceFrom;

		public float sliceTo;

		public SphereCutOption cutTpye;

		public float cutFrom;

		public float cutTo;

		[Header("Mapping Coordinates")]
		public bool generateMappingCoords;

		public bool realWorldMapSize;

		public Vector2 UVOffset;

		public Vector2 UVTiling;

		[Header("Others")]
		public bool flipNormals;

		public bool smooth;

		protected override void CreateMesh()
		{
		}
	}
}
