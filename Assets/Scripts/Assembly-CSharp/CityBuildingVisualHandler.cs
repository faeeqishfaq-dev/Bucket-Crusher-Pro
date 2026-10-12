using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CityBuildingVisualHandler : MonoBehaviour
{
	public List<GameObject> Levels;

	public Transform Anim;

	private Tween _currentTween;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void OnRvx2(bool enable)
	{
	}

	public void SetLevel(int level)
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void Upgrade()
	{
	}

	public void DoUpgradeAnim()
	{
	}

	[ExposeMethodInEditor(false, false)]
	public void ResetLevel0()
	{
	}

	private void Reset()
	{
	}
}
