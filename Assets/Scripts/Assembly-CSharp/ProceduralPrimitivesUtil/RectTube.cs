using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class RectTube : PPBase
	{
		[Header("Basic parameters")]
		public float width1;

		public float width2;

		public float length1;

		public float length2;

		public float height;

		public bool cap1;

		public float capThickness1;

		public bool cap2;

		public float capThickness2;

		[Header("Segments")]
		public int widthSegs;

		public int lengthSegs;

		public int heightSegs;

		public int capSegs;

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
