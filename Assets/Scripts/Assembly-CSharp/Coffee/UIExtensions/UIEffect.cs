using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/UIEffect/UIEffect", 1)]
	public class UIEffect : UIEffectBase
	{
		public enum BlurEx
		{
			None = 0,
			Ex = 1
		}

		public const string shaderName = "UI/Hidden/UI-Effect";

		private static readonly ParameterTexture _ptex;

		[FormerlySerializedAs("m_ToneLevel")]
		[Tooltip("Effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor;

		[Tooltip("Color effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ColorFactor;

		[FormerlySerializedAs("m_Blur")]
		[Tooltip("How far is the blurring from the graphic.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor;

		[FormerlySerializedAs("m_ToneMode")]
		[Tooltip("Effect mode")]
		[SerializeField]
		private EffectMode m_EffectMode;

		[Tooltip("Color effect mode")]
		[SerializeField]
		private ColorMode m_ColorMode;

		[Tooltip("Blur effect mode")]
		[SerializeField]
		private BlurMode m_BlurMode;

		[Tooltip("Advanced blurring remove common artifacts in the blur effect for uGUI.")]
		[SerializeField]
		private bool m_AdvancedBlur;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ShadowBlur;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private ShadowStyle m_ShadowStyle;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Color m_ShadowColor;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Vector2 m_EffectDistance;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private bool m_UseGraphicAlpha;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private Color m_EffectColor;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private List<UIShadow.AdditionalShadow> m_AdditionalShadows;

		public override AdditionalCanvasShaderChannels requiredChannels => default(AdditionalCanvasShaderChannels);

		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float toneLevel
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float effectFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float colorFactor
		{
			get
			{
				return 0f;
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

		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
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

		[Obsolete("Use effectMode instead (UnityUpgradable) -> effectMode")]
		public EffectMode toneMode => default(EffectMode);

		public EffectMode effectMode => default(EffectMode);

		public ColorMode colorMode => default(ColorMode);

		public BlurMode blurMode => default(BlurMode);

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

		public override ParameterTexture ptex => null;

		public bool advancedBlur => false;

		public override void ModifyMesh(VertexHelper vh)
		{
		}

		protected override void SetDirty()
		{
		}

		private static void GetBounds(List<UIVertex> verts, int start, int count, ref Rect posBounds, ref Rect uvBounds, bool global)
		{
		}
	}
}
