using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class ChamferBox : PPBase
	{
		[Header("Basic parameters")]
		public float width;

		public float length;

		public float height;

		public float fillet;

		[Header("Segments")]
		public int widthSegs;

		public int lengthSegs;

		public int heightSegs;

		public int filletSegs;

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
