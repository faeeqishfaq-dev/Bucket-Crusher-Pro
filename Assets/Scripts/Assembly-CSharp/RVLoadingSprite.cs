using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RVLoadingSprite : MonoBehaviour
{
	[SerializeField]
	public Sprite LoadingSprite;

	public Image DefaultImage;

	public Button RVButton;

	public bool HandleInteractivity;

	private Tween _currentTween;

	private Sprite _defaultSprite;

	private Color _defaultColor;

	private bool _suscribed;

	private void Awake()
	{
		if (RVButton != null) RVButton.interactable = true;
	}

	private void OnEnable()
	{
		if (RVButton != null) RVButton.interactable = true;
	}

	private void PrepareCheckRV()
	{
		if (RVButton != null) RVButton.interactable = true;
	}

	private void DoCheckRV()
	{
		if (RVButton != null) RVButton.interactable = true;
	}

	private void OnDisable()
	{
	}

	private void OnRVReady(bool success)
	{
		if (RVButton != null) RVButton.interactable = true;
	}
}
