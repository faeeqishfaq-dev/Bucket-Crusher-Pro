using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[AddComponentMenu("UI/UIEffect/UIEffectCapturedImage", 200)]
	public class UIEffectCapturedImage : RawImage
	{
		public enum DesamplingRate
		{
			None = 0,
			x1 = 1,
			x2 = 2,
			x4 = 4,
			x8 = 8
		}

		[CompilerGenerated]
		private sealed class _003C_CoUpdateTextureOnNextFrame_003Ed__92 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIEffectCapturedImage _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003C_CoUpdateTextureOnNextFrame_003Ed__92(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public const string shaderName = "UI/Hidden/UI-EffectCapture";

		[Tooltip("Effect factor between 0(no effect) and 1(complete effect).")]
		[FormerlySerializedAs("m_ToneLevel")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_EffectFactor;

		[Tooltip("Color effect factor between 0(no effect) and 1(complete effect).")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ColorFactor;

		[Tooltip("How far is the blurring from the graphic.")]
		[FormerlySerializedAs("m_Blur")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_BlurFactor;

		[Tooltip("Effect mode.")]
		[FormerlySerializedAs("m_ToneMode")]
		[SerializeField]
		private EffectMode m_EffectMode;

		[Tooltip("Color effect mode.")]
		[SerializeField]
		private ColorMode m_ColorMode;

		[Tooltip("Blur effect mode.")]
		[SerializeField]
		private BlurMode m_BlurMode;

		[Tooltip("Color for the color effect.")]
		[SerializeField]
		private Color m_EffectColor;

		[Tooltip("Desampling rate of the generated RenderTexture.")]
		[SerializeField]
		private DesamplingRate m_DesamplingRate;

		[Tooltip("Desampling rate of reduction buffer to apply effect.")]
		[SerializeField]
		private DesamplingRate m_ReductionRate;

		[Tooltip("FilterMode for capturing.")]
		[SerializeField]
		private FilterMode m_FilterMode;

		[Tooltip("Effect material.")]
		[SerializeField]
		private Material m_EffectMaterial;

		[Tooltip("Blur iterations.")]
		[FormerlySerializedAs("m_Iterations")]
		[SerializeField]
		[Range(1f, 8f)]
		private int m_BlurIterations;

		[Tooltip("Fits graphic size to screen on captured.")]
		[FormerlySerializedAs("m_KeepCanvasSize")]
		[SerializeField]
		private bool m_FitToScreen;

		[Tooltip("Capture automatically on enable.")]
		[SerializeField]
		private bool m_CaptureOnEnable;

		private RenderTexture _rt;

		private RenderTargetIdentifier _rtId;

		private static int s_CopyId;

		private static int s_EffectId1;

		private static int s_EffectId2;

		private static int s_EffectFactorId;

		private static int s_ColorFactorId;

		private static CommandBuffer s_CommandBuffer;

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

		public virtual Material effectMaterial => null;

		public DesamplingRate desamplingRate
		{
			get
			{
				return default(DesamplingRate);
			}
			set
			{
			}
		}

		public DesamplingRate reductionRate
		{
			get
			{
				return default(DesamplingRate);
			}
			set
			{
			}
		}

		public FilterMode filterMode
		{
			get
			{
				return default(FilterMode);
			}
			set
			{
			}
		}

		public RenderTexture capturedTexture => null;

		[Obsolete("Use blurIterations instead (UnityUpgradable) -> blurIterations")]
		public int iterations
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int blurIterations
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[Obsolete("Use fitToScreen instead (UnityUpgradable) -> fitToScreen")]
		public bool keepCanvasSize
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool fitToScreen
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
		public RenderTexture targetTexture
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public bool captureOnEnable
		{
			get
			{
				return false;
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

		protected override void OnDestroy()
		{
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
		}

		public void GetDesamplingSize(DesamplingRate rate, out int w, out int h)
		{
			w = default(int);
			h = default(int);
		}

		public void Capture()
		{
		}

		private void SetupCommandBuffer()
		{
		}

		public void Release()
		{
		}

		private void _Release(bool releaseRT)
		{
		}

		[Conditional("UNITY_EDITOR")]
		private void _SetDirty()
		{
		}

		private void _Release(ref RenderTexture obj)
		{
		}

		[IteratorStateMachine(typeof(_003C_CoUpdateTextureOnNextFrame_003Ed__92))]
		private IEnumerator _CoUpdateTextureOnNextFrame()
		{
			return null;
		}

		private void UpdateTexture()
		{
		}
	}
}
