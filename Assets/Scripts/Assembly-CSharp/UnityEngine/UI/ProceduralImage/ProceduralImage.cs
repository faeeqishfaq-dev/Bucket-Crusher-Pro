using System;

namespace UnityEngine.UI.ProceduralImage
{
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Procedural Image")]
	public class ProceduralImage : Image
	{
		[SerializeField]
		private float borderWidth;

		private ProceduralImageModifier modifier;

		private static Material materialInstance;

		[SerializeField]
		private float falloffDistance;

		private static Material DefaultProceduralImageMaterial
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public float BorderWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float FalloffDistance
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected ProceduralImageModifier Modifier
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public System.Type ModifierType
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public override Material material
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}

		private void Init()
		{
		}

		protected void OnVerticesDirty()
		{
		}

		protected void FixTexCoordsInCanvas()
		{
		}

		protected void FixTexCoordsInCanvas(Canvas c)
		{
		}

		private Vector4 FixRadius(Vector4 vec)
		{
			return default(Vector4);
		}

		protected override void OnPopulateMesh(VertexHelper toFill)
		{
		}

		protected override void OnTransformParentChanged()
		{
		}

		private ProceduralImageInfo CalculateInfo()
		{
			return default(ProceduralImageInfo);
		}

		private void EncodeAllInfoIntoVertices(VertexHelper vh, ProceduralImageInfo info)
		{
		}

		private float EncodeFloats_0_1_16_16(float a, float b)
		{
			return 0f;
		}
	}
}
