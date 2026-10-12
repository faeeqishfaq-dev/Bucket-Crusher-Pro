using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPrimitivesUtil
{
	[ExecuteInEditMode]
	public abstract class PPBase : MonoBehaviour
	{
		protected List<Vector3> m_vertices;

		protected List<int> m_triangles;

		protected List<Vector3> m_normals;

		protected List<Vector2> m_uv;

		private bool m_dirty;

		private MeshFilter m_meshFilter;

		protected Mesh m_mesh;

		public Vector3 pivotOffset;

		public Vector3 rotation;

		protected Quaternion m_rotation;

		public bool isDirty => false;

		public Mesh mesh => null;

		public static float PointLineDistance(Vector3 vp, Vector3 v)
		{
			return 0f;
		}

		public static float PointLineProjectionn(Vector3 vp, Vector3 v)
		{
			return 0f;
		}

		protected void CreateTriangle(Vector3 p0, Vector3 p1, Vector3 p2)
		{
		}

		protected void CreateTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector2 p0uv, Vector2 p1uv, Vector2 p2uv)
		{
		}

		protected void CreateQuad(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
		}

		protected void CreateQuad(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector2 p0uv, Vector2 p1uv, Vector2 p2uv, Vector2 p3uv)
		{
		}

		protected void CreateTriangle(Vector3 center, Vector3 forward, Vector3 right, float width, float length, float offset, int seg, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateTriangle(Vector3 center, Vector3 forward, Vector3 right, float width, float length, float offset, int seg, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreateTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int seg, bool generateUV, Vector2 uvOffset, Vector2 uvTiling)
		{
		}

		protected void CreateTriangle(Vector3 center, Vector3 forward, Vector3 right, float width, float length, float offset, int segW, int segL, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateTriangle(Vector3 center, Vector3 forward, Vector3 right, float width, float length, float offset, int segW, int segL, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreateTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int segW, int segL, bool generateUV, Vector2 uvOffset, Vector2 uvTiling)
		{
		}

		protected void CreatePlane(Vector3 center, Vector3 forward, Vector3 right, float width, float length, int segW, int segL, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreatePlane(Vector3 center, Vector3 forward, Vector3 right, float width, float length, int segW, int segL, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreatePlane(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 normal, int segW, int segL, bool generateUV, Vector2 uvOffset, Vector2 uvTiling)
		{
		}

		protected void CreateTrapezoid(Vector3 center, Vector3 forward, Vector3 right, float width1, float width2, float length, float offset, int segW, int segL, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateTrapezoid(Vector3 center, Vector3 forward, Vector3 right, float width1, float width2, float length, float offset, int segW, int segL, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreateTrapezoid(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 normal, int segW, int segL, bool generateUV, Vector2 uvOffset, Vector2 uvTiling)
		{
		}

		protected void CreateCircle(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateCircle(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateCircle(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreateHemiCircle(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool hemiCircle, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, bool flip, bool isSector = false)
		{
		}

		protected void CreateHemiCircle(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool hemiCircle, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool isSector = false)
		{
		}

		protected void CreateCircle(Vector3[] points, Vector2[] uvs, Vector3 center, Vector3 normal, int seg, Vector2 uvOffset, Vector2 uvTiling, float centerUVOffset = 0f)
		{
		}

		protected void CreateRing(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateRing(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip)
		{
		}

		protected void CreateRing(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip)
		{
		}

		protected void CreateRing(Vector3[] points, Vector2[] uvs, Vector3 center, Vector3 normal, float ratio, int seg, Vector2 uvOffset, Vector2 uvTiling, float centerUVOffset = 0f)
		{
		}

		protected void CreateHemiRing(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool hemiCircle, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, bool flip, bool isSector = false)
		{
		}

		protected void CreateHemiRing(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool hemiCircle, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool isSector = false)
		{
		}

		protected void CreateRing(Vector3[] points, Vector3[] points2, Vector2[] uvs, Vector2[] uvs2, Vector3 center, Vector3 normal, int seg, Vector2 uvOffset, Vector2 uvTiling, float centerUVOffset = 0f)
		{
		}

		protected void CreateTorus(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip, bool smooth, float sectionSliceFrom = 0f, float sectionSliceTo = 360f)
		{
		}

		protected void CreateTorus(Vector3 center, Vector3 forward, Vector3 right, float radius1, float radius2, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth, float sectionSliceFrom, float sectionSliceTo)
		{
		}

		protected void CreateTorus(Vector3[][] points, Vector3[] centers, int seg, bool generateUV, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateCylinder(Vector3 center, Vector3 forward, Vector3 right, float height, float radius, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip, bool smooth)
		{
		}

		protected void CreateCylinder(Vector3 center, Vector3 forward, Vector3 right, float height, float radius, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateCylinder(Vector3[] pDown, Vector3[] pUp, Vector3 centerDown, Vector3 centerUp, int seg, bool generateUV, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateCone(Vector3 center, Vector3 forward, Vector3 right, float height, float radius1, float radius2, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip, bool smooth)
		{
		}

		protected void CreateCone(Vector3 center, Vector3 forward, Vector3 right, float height, float radius1, float radius2, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateCone(Vector3 centerDown, Vector3 centerUp, Vector3 forward, Vector3 right, float height, float radius1, float radius2, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, bool flip, bool smooth)
		{
		}

		protected void CreateCone(Vector3 centerDown, Vector3 centerUp, Vector3 forward, Vector3 right, float height, float radius1, float radius2, int sides, int segHeight, bool sliceOn, float sliceFrom, float sliceTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateCone(Vector3[] pDown, Vector3[] pUp, Vector3 centerDown, Vector3 centerUp, Vector3 up, int seg, bool generateUV, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateSphere(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool hemiSphere, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, bool flip, bool smooth)
		{
		}

		protected void CreateSphere(Vector3 center, Vector3 forward, Vector3 right, float radius, int sides, int seg, bool sliceOn, float sliceFrom, float sliceTo, bool hemiSphere, float cutFrom, float cutTo, bool generateUV, bool realWorldMapSize, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth)
		{
		}

		protected void CreateSphere(Vector3[][] points, Vector3 center, bool generateUV, Vector2 uvOffset, Vector2 uvTiling, bool flip, bool smooth, float rFrom, float rTo)
		{
		}

		protected abstract void CreateMesh();

		public void Apply()
		{
		}

		private void OnDestroy()
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void Reset()
		{
		}

		private void OnValidate()
		{
		}

		public GameObject CreateInstance()
		{
			return null;
		}

		public void ForceRecreateMesh()
		{
		}
	}
}
