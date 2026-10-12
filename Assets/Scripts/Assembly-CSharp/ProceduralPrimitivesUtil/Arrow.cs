using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Arrow : PPBase
	{
		[Header("Basic parameters")]
		public float width1;

		public float width2;

		public float width3;

		public float length1;

		public float length2;

		public float height;

		[Header("Segments")]
		public int widthSegs1;

		public int lengthSegs1;

		public int widthSegs2;

		public int lengthSegs2;

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
