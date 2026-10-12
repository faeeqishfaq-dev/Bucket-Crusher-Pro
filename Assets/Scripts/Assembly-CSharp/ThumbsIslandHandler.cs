using UnityEngine;

public class ThumbsIslandHandler : MonoBehaviour
{
	public GameObject CameraDock;

	public Transform BuildingParent;

	public CityBuildingVisualHandler CurrentBuilding;

	public bool IsUpgradedIsland;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnElementSelected(CityBuildingElementHandler obj)
	{
	}
}
