using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[AddComponentMenu("UI/UIEffect/UIDissolve", 3)]
	public class UIDissolve : UIEffectBase
	{
		public const string shaderName = "UI/Hidden/UI-Effect-Dissolve";

		private static readonly ParameterTexture _ptex;

		[Tooltip("Current location[0-1] for dissolve effect. 0 is not dissolved, 1 is completely dissolved.")]
		[FormerlySerializedAs("m_Location")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor;

		[Tooltip("Edge width.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Width;

		[Tooltip("Edge softness.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Softness;

		[Tooltip("Edge color.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_Color;

		[Tooltip("Edge color effect mode.")]
		[SerializeField]
		private ColorMode m_ColorMode;

		[Tooltip("Noise texture for dissolving (single channel texture).")]
		[SerializeField]
		private Texture m_NoiseTexture;

		[Header("Advanced Option")]
		[Tooltip("The area for effect.")]
		[SerializeField]
		protected EffectArea m_EffectArea;

		[Tooltip("Keep effect aspect ratio.")]
		[SerializeField]
		private bool m_KeepAspectRatio;

		[Header("Effect Player")]
		[SerializeField]
		private EffectPlayer m_Player;

		[Tooltip("Reverse the dissolve effect.")]
		[FormerlySerializedAs("m_ReverseAnimation")]
		[SerializeField]
		private bool m_Reverse;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		[Range(0.1f, 10f)]
		private float m_Duration;

		[Obsolete]
		[HideInInspector]
		[SerializeField]
		private AnimatorUpdateMode m_UpdateMode;

		private MaterialCache _materialCache;

		[Obsolete("Use effectFactor instead (UnityUpgradable) -> effectFactor")]
		public float location
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

		public float width
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float softness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Color color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public Texture noiseTexture
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public EffectArea effectArea
		{
			get
			{
				return default(EffectArea);
			}
			set
			{
			}
		}

		public bool keepAspectRatio
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public ColorMode colorMode => default(ColorMode);

		[Obsolete("Use Play/Stop method instead")]
		public bool play
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		[Obsolete]
		public bool loop
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public float duration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete]
		public float loopDelay
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public AnimatorUpdateMode updateMode
		{
			get
			{
				return default(AnimatorUpdateMode);
			}
			set
			{
			}
		}

		public bool reverse
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public override ParameterTexture ptex => null;

		private EffectPlayer _player => null;

		public override void ModifyMaterial()
		{
		}

		public override void ModifyMesh(VertexHelper vh)
		{
		}

		protected override void SetDirty()
		{
		}

		public void Play(bool reset = true)
		{
		}

		public void Stop(bool reset = true)
		{
		}

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}
	}
}
