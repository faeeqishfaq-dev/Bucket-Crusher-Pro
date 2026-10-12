using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[AddComponentMenu("UI/UIEffect/UITransitionEffect", 5)]
	public class UITransitionEffect : UIEffectBase
	{
		public enum EffectMode
		{
			Fade = 1,
			Cutoff = 2,
			Dissolve = 3
		}

		public const string shaderName = "UI/Hidden/UI-Effect-Transition";

		private static readonly ParameterTexture _ptex;

		[Tooltip("Effect mode.")]
		[SerializeField]
		private EffectMode m_EffectMode;

		[Tooltip("Effect factor between 0(hidden) and 1(shown).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor;

		[Tooltip("Transition texture (single channel texture).")]
		[SerializeField]
		private Texture m_TransitionTexture;

		[Header("Advanced Option")]
		[Tooltip("The area for effect.")]
		[SerializeField]
		private EffectArea m_EffectArea;

		[Tooltip("Keep effect aspect ratio.")]
		[SerializeField]
		private bool m_KeepAspectRatio;

		[Tooltip("Dissolve edge width.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_DissolveWidth;

		[Tooltip("Dissolve edge softness.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_DissolveSoftness;

		[Tooltip("Dissolve edge color.")]
		[SerializeField]
		[ColorUsage(false)]
		private Color m_DissolveColor;

		[Tooltip("Disable graphic's raycast target on hidden.")]
		[SerializeField]
		private bool m_PassRayOnHidden;

		[Header("Effect Player")]
		[SerializeField]
		private EffectPlayer m_Player;

		private MaterialCache _materialCache;

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

		public Texture transitionTexture
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public EffectMode effectMode => default(EffectMode);

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

		public override ParameterTexture ptex => null;

		public float dissolveWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float dissolveSoftness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Color dissolveColor
		{
			get
			{
				return default(Color);
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

		public bool passRayOnHidden
		{
			get
			{
				return false;
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

		private EffectPlayer _player => null;

		public void Show(bool reset = true)
		{
		}

		public void Hide(bool reset = true)
		{
		}

		public override void ModifyMaterial()
		{
		}

		public override void ModifyMesh(VertexHelper vh)
		{
		}

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}

		protected override void SetDirty()
		{
		}
	}
}
