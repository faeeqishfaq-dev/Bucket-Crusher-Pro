using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[AddComponentMenu("UI/UIEffect/UIHsvModifier", 4)]
	public class UIHsvModifier : UIEffectBase
	{
		public const string shaderName = "UI/Hidden/UI-Effect-HSV";

		private static readonly ParameterTexture _ptex;

		[Header("Target")]
		[Tooltip("Target color to affect hsv shift.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_TargetColor;

		[Tooltip("Color range to affect hsv shift [0 ~ 1].")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Range;

		[Header("Adjustment")]
		[Tooltip("Hue shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Hue;

		[Tooltip("Saturation shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Saturation;

		[Tooltip("Value shift [-0.5 ~ 0.5].")]
		[SerializeField]
		[Range(-0.5f, 0.5f)]
		private float m_Value;

		public Color targetColor
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public float range
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float saturation
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float value
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float hue
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public override ParameterTexture ptex => null;

		public override void ModifyMesh(VertexHelper vh)
		{
		}

		protected override void SetDirty()
		{
		}
	}
}
