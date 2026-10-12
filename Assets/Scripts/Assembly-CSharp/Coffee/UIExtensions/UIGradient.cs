using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/MeshEffectForTextMeshPro/UIGradient", 101)]
	public class UIGradient : BaseMeshEffect
	{
		public enum Direction
		{
			Horizontal = 0,
			Vertical = 1,
			Angle = 2,
			Diagonal = 3
		}

		public enum GradientStyle
		{
			Rect = 0,
			Fit = 1,
			Split = 2
		}

		private struct Matrix2x3
		{
			public float m00;

			public float m01;

			public float m02;

			public float m10;

			public float m11;

			public float m12;

			public Matrix2x3(Rect rect, float cos, float sin)
			{
				m00 = 0f;
				m01 = 0f;
				m02 = 0f;
				m10 = 0f;
				m11 = 0f;
				m12 = 0f;
			}

			public static Vector2 operator *(Matrix2x3 m, Vector2 v)
			{
				return default(Vector2);
			}
		}

		[Tooltip("Gradient Direction.")]
		[SerializeField]
		private Direction m_Direction;

		[Tooltip("Color1: Top or Left.")]
		[SerializeField]
		private Color m_Color1;

		[Tooltip("Color2: Bottom or Right.")]
		[SerializeField]
		private Color m_Color2;

		[Tooltip("Color3: For diagonal.")]
		[SerializeField]
		private Color m_Color3;

		[Tooltip("Color4: For diagonal.")]
		[SerializeField]
		private Color m_Color4;

		[Tooltip("Gradient rotation.")]
		[SerializeField]
		[Range(-180f, 180f)]
		private float m_Rotation;

		[Tooltip("Gradient offset for Horizontal, Vertical or Angle.")]
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Offset1;

		[Tooltip("Gradient offset for Diagonal.")]
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_Offset2;

		[Tooltip("Gradient style for Text.")]
		[SerializeField]
		private GradientStyle m_GradientStyle;

		[Tooltip("Color space to correct color.")]
		[SerializeField]
		private ColorSpace m_ColorSpace;

		[Tooltip("Ignore aspect ratio.")]
		[SerializeField]
		private bool m_IgnoreAspectRatio;

		private static readonly Vector2[] s_SplitedCharacterPosition;

		public Graphic targetGraphic => null;

		public Direction direction
		{
			get
			{
				return default(Direction);
			}
			set
			{
			}
		}

		public Color color1
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color color2
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color color3
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Color color4
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public float rotation
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float offset
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Vector2 offset2
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public GradientStyle gradientStyle
		{
			get
			{
				return default(GradientStyle);
			}
			set
			{
			}
		}

		public ColorSpace colorSpace
		{
			get
			{
				return default(ColorSpace);
			}
			set
			{
			}
		}

		public bool ignoreAspectRatio
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public override void ModifyMesh(VertexHelper vh)
		{
		}
	}
}
