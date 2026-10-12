using System.Collections.Generic;
using UnityEngine;

public class RailSizeHandler : MonoBehaviour
{
	public List<GameObject> ObjectPerLevel;

	public Transform TransformToScale;

	public ConfigurableJoint JointToLimit;

	public GameObject FX;

	private static int _lastSize;

	private void Start()
	{
	}

	public void UpdateSize()
	{
	}
}
