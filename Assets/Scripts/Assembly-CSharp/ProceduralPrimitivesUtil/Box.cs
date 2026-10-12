using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Box : PPBase
	{
		[Header("Basic parameters")]
		public float width;

		public float length;

		public float height;

		[Header("Segments")]
		public int widthSegs;

		public int lengthSegs;

		public int heightSegs;

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
