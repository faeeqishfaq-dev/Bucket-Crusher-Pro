using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ContentSizeFitter))]
public class ContentSizeFitterMaxWidth : MonoBehaviour
{
	public float maxWidth;

	private RectTransform _rtfm;

	private ContentSizeFitter _fitter;

	private ILayoutElement _layout;

	private void Awake()
	{
	}

	private void Update()
	{
	}
}
