using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Cone : PPBase
	{
		[Header("Basic parameters")]
		public float radius1;

		public float radius2;

		public float height;

		[Header("Segments")]
		public int sides;

		public int capSegs;

		public int heightSegs;

		[Header("Slice")]
		public bool sliceOn;

		public float sliceFrom;

		public float sliceTo;

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
