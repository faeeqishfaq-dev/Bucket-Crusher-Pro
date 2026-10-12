using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	public class Prism : PPBase
	{
		[Header("Basic parameters")]
		public float width;

		public float length;

		public float height;

		public float offset;

		[Header("Segments")]
		public int sideSegs;

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
