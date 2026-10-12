using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class ChamferCylinder : PPBase
	{
		[Header("Basic parameters")]
		public float radius;

		public float height;

		public float fillet;

		[Header("Segments")]
		public int sides;

		public int capSegs;

		public int heightSegs;

		public int filletSegs;

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
