using System.Collections.Generic;
using UnityEngine;

public class CityBuildingElementHandler : MonoBehaviour
{
	public CityBuildingVisualHandler Visual;

	public GameObject LockedDock;

	public GameObject SelectionVisual;

	public CityBuildingUnlockTooltip UnlockToolTip;

	public CityBuildingCollectTooltip CollectToolTip;

	public int BuildId;

	public ElementsDataHolder ElementsDataHolder;

	public CityBuildingElementData Data;

	public List<GameObject> LockedBricks;

	public GameObject UnlockFX;

	public GameObject X2FX;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnRvx2(bool enable)
	{
	}

	private void OnDestroy()
	{
	}

	private void OnElementSelected(CityBuildingElementHandler elem)
	{
	}

	private void Update()
	{
	}

	public void Refresh()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void Select()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void DoReset()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void Unlock()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void LevelUp()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void DoCollect()
	{
	}
}
