using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[RequireComponent(typeof(Graphic))]
	[AddComponentMenu("UI/UIEffect/UIShadow", 100)]
	public class UIShadow : BaseMeshEffect, IParameterTexture
	{
		[Serializable]
		[Obsolete]
		public class AdditionalShadow
		{
			[FormerlySerializedAs("shadowBlur")]
			[Range(0f, 1f)]
			public float blur;

			[FormerlySerializedAs("shadowMode")]
			public ShadowStyle style;

			[FormerlySerializedAs("shadowColor")]
			public Color effectColor;

			public Vector2 effectDistance;

			public bool useGraphicAlpha;
		}

		[Tooltip("How far is the blurring shadow from the graphic.")]
		[FormerlySerializedAs("m_Blur")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor;

		[Tooltip("Shadow effect style.")]
		[SerializeField]
		private ShadowStyle m_Style;

		[HideInInspector]
		[Obsolete]
		[SerializeField]
		private List<AdditionalShadow> m_AdditionalShadows;

		[SerializeField]
		private Color m_EffectColor;

		[SerializeField]
		private Vector2 m_EffectDistance;

		[SerializeField]
		private bool m_UseGraphicAlpha;

		private const float kMaxEffectDistance = 600f;

		private int _graphicVertexCount;

		private static readonly List<UIShadow> tmpShadows;

		private UIEffect _uiEffect;

		private static readonly List<UIVertex> s_Verts;

		public Color effectColor
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Vector2 effectDistance
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public bool useGraphicAlpha
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete("Use blurFactor instead (UnityUpgradable) -> blurFactor")]
		public float blur
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float blurFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public ShadowStyle style
		{
			get
			{
				return default(ShadowStyle);
			}
			set
			{
			}
		}

		public int parameterIndex { get; set; }

		public ParameterTexture ptex { get; private set; }

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}

		public override void ModifyMesh(VertexHelper vh)
		{
		}

		private void _ApplyShadow(List<UIVertex> verts, Color color, ref int start, ref int end, Vector2 effectDistance, ShadowStyle style, bool useGraphicAlpha)
		{
		}

		private void _ApplyShadowZeroAlloc(List<UIVertex> verts, Color color, ref int start, ref int end, float x, float y, bool useGraphicAlpha)
		{
		}

		private void _SetDirty()
		{
		}
	}
}
