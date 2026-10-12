using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OldGenericRewardAnimator : MonoBehaviour
{
	[Serializable]
	public class EditorReferences
	{
		public ImageAnimationItem BaseItem;

		public float CircleSize;

		public TextAnimatedItem BaseText;

		public RectTransform DefaultLayer;

		public RectTransform DefaultOrigin;

		public RectTransform CashDefaultDestination;

		public RectTransform GemDefaultDestination;
	}

	[SerializeField]
	public EditorReferences references;

	private Queue<ImageAnimationItem> _readyItems;

	private List<ImageAnimationItem> _movingItems;

	private Queue<TextAnimatedItem> _readyTexts;

	private List<TextAnimatedItem> _movingTexts;

	private List<Sequence> _playingSequences;

	private static OldGenericRewardAnimator _instance;

	public static OldGenericRewardAnimator Instance => null;

	private void Awake()
	{
	}

	public void OnDisable()
	{
	}

	public void Clear()
	{
	}

	private void AnimateSingleCurrency(TextAnimatedItem text, List<ImageAnimationItem> currencyIcons, RectTransform destination, Action displayUpdateFunction, Action finalCallback, float delay = 0f)
	{
	}

	public void AnimateCurrency(int qty, int steps, RectTransform origin, RectTransform destination, Vector2 size, bool useText, bool useCurrency, Action tickCallback, Action finalCallback, float delay, Transform parent, float circleSize = 200f, Sprite icon = null, bool useRandomRot = false)
	{
	}

	private Transform FindRootCanvasTransform(Transform t)
	{
		return null;
	}

	public void SetDefaultOriginPosition(Vector2 position)
	{
	}
}
