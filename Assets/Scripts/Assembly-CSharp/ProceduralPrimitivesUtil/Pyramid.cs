using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Pyramid : PPBase
	{
		[Header("Basic parameters")]
		public float width1;

		public float length1;

		public float width2;

		public float length2;

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
