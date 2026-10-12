using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coffee.UIExtensions
{
	[Serializable]
	public class EffectPlayer
	{
		[Header("Effect Player")]
		[Tooltip("Playing.")]
		public bool play;

		[Tooltip("Initial play delay.")]
		[Range(0f, 10f)]
		public float initialPlayDelay;

		[Tooltip("Duration.")]
		[Range(0.01f, 10f)]
		public float duration;

		[Tooltip("Loop.")]
		public bool loop;

		[Tooltip("Delay before looping.")]
		[Range(0f, 10f)]
		public float loopDelay;

		[Tooltip("Update mode")]
		public AnimatorUpdateMode updateMode;

		private static List<Action> s_UpdateActions;

		private float _time;

		private Action<float> _callback;

		public void OnEnable(Action<float> callback = null)
		{
		}

		public void OnDisable()
		{
		}

		public void Play(bool reset, Action<float> callback = null)
		{
		}

		public void Stop(bool reset)
		{
		}

		private void OnWillRenderCanvases()
		{
		}
	}
}
