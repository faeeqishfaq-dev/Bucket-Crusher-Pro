using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Triangle : PPBase
	{
		[Header("Basic parameters")]
		public float width;

		public float length;

		public float offset;

		[Header("Segments")]
		public int widthSegs;

		public int lengthSegs;

		[Header("Mapping Coordinates")]
		public bool generateMappingCoords;

		public bool realWorldMapSize;

		public Vector2 UVOffset;

		public Vector2 UVTiling;

		[Header("Others")]
		public bool flipNormals;

		public bool uniformTriangle;

		protected override void CreateMesh()
		{
		}
	}
}
