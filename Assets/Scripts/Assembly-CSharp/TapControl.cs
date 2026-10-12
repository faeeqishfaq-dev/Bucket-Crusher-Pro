using UnityEngine;

public class TapControl : MonoBehaviour, MoveDirection
{
	private Vector2 direction;

	private int layerMask;

	private Vector2 currentTarget;

	private bool canControl;

	[SerializeField]
	private Transform wayTrajectory;

	[SerializeField]
	private SpriteRenderer[] directionSprites;

	[SerializeField]
	private Color maxDistanceColor;

	[SerializeField]
	private Color minDistanceColor;

	[SerializeField]
	private GameObject _guide;

	public Transform _headPos;

	public bool tapControlChosen;

	public bool valid;

	public bool pressed;

	public Vector2 Direction => direction;

	public void StartControl()
	{
		canControl = true;
		int level = GameManager.Instance != null ? GameManager.Instance.Currentlevel : PlayerPrefs.GetInt("Level", 1);
		if (_guide != null && PlayerPrefs.GetInt("TapTutorialDone", 0) == 0 && level < 3)
		{
			_guide.SetActive(true);
		}
	}

	public void StopControl()
	{
		canControl = false;
		if (_guide != null)
		{
			_guide.SetActive(false);
		}
	}

	private void Start()
	{
		if (_guide != null)
		{
			int level = GameManager.Instance != null ? GameManager.Instance.Currentlevel : PlayerPrefs.GetInt("Level", 1);
			bool done = PlayerPrefs.GetInt("TapTutorialDone", 0) == 1 || level >= 3;
			_guide.SetActive(!done);
		}
	}

	private bool hasReceivedFirstGameplayTap;

	private void Update()
	{
		bool isPlaying = GameManager.Instance != null && GameManager.Instance.IsPlaying;
		if (isPlaying && !hasReceivedFirstGameplayTap)
		{
			if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
			{
				hasReceivedFirstGameplayTap = true;
				if (_guide != null && _guide.activeSelf)
				{
					_guide.SetActive(false);
				}
			}
		}
	}

	private void FixedUpdate()
	{
	}
}
