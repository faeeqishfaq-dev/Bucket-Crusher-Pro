using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Tube : PPBase
	{
		[Header("Basic parameters")]
		public float radius1;

		public float radius2;

		public float height;

		public bool cap1;

		public float capThickness1;

		public bool cap2;

		public float capThickness2;

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
