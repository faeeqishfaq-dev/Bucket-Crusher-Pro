using UnityEngine;
using Object = UnityEngine.Object;

public class Cam : MonoBehaviour
{
	public Transform _head;

	private float startXPos;

	private float lastXPos;

	private float startYPos;

	private float lastYPos;

	private LevelOrientationCameraFields rightcamera;

	private LevelOrientationCameraFields leftcamera;

	private LevelOrientationCameraFields topcamera;

	private LevelOrientationCameraFields archcamera;

	private LevelOrientationCameraFields currentCamera;

	private new Camera camera;

	private Vector3 BasePosition;

	public Transform custom;

	public float originalFOV = 60f;

	public float customFOV = 40f;

	public float lerpAmount = 5f;

	public Transform zoomedOut;

	private bool followHead = false;

	public Camera cameraObject => camera != null ? camera : (camera = GetComponent<Camera>());

	private void Awake()
	{
		camera = GetComponent<Camera>();
		if (camera == null) camera = GetComponentInChildren<Camera>();
		BasePosition = transform.position;
		if (camera != null) originalFOV = camera.fieldOfView;
	}

	private void Start()
	{
		if (_head == null)
		{
			Head h = Object.FindAnyObjectByType<Head>();
			if (h != null) _head = h.transform;
		}
	}

	public void SetHead(Transform head)
	{
		_head = head;
		followHead = true;
	}

	public void SetCameraSide(LevelPosType t)
	{
	}

	public void ShowWholeLevel()
	{
		followHead = false;
		if (zoomedOut != null)
		{
			transform.position = zoomedOut.position;
			transform.rotation = zoomedOut.rotation;
		}
		else
		{
			transform.position = BasePosition;
		}
	}

	internal void ForceFollowCustom()
	{
		followHead = true;
	}

	private void Update()
	{
		if (followHead && _head != null)
		{
			Vector3 target = new Vector3(_head.position.x, _head.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * lerpAmount);
		}
	}

	public void SetZeroPos()
	{
		transform.position = BasePosition;
	}
}
