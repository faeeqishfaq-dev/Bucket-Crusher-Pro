using UnityEngine;

public class PanZoom : MonoBehaviour
{
	private Vector3 touchStart;

	public float OrthoZoomOutMin;

	public float OrthoZoomOutMax;

	public float NonOrthoFovMin;

	public float NonOrthoFovMax;

	public Vector2 MinCamPos;

	public Vector2 MaxCamPos;

	public float XMultiply;

	public float YMultiply;

	public float ScrollMultiply;

	public float SnapMultiply;

	private Camera _camera;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void zoom(float increment)
	{
	}
}
