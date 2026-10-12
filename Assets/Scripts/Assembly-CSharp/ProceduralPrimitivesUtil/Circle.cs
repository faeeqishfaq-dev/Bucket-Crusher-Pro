using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Circle : PPBase
	{
		[Header("Basic parameters")]
		public float radius;

		[Header("Segments")]
		public int sides;

		public int segments;

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

		protected override void CreateMesh()
		{
		}
	}
}
