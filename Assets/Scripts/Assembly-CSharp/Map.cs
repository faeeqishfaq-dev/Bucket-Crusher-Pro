using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	private List<Transform> blocks;

	private List<Vector3> blocksPoses;

	public List<Transform> Blocks => blocks;

	private void Awake()
	{
		if (gameObject.GetComponent<VoxelWallConverter>() == null)
		{
			gameObject.AddComponent<VoxelWallConverter>();
		}
	}

	public void Processs()
	{
		if (blocks == null) blocks = new List<Transform>();
		else blocks.Clear();

		if (blocksPoses == null) blocksPoses = new List<Vector3>();
		else blocksPoses.Clear();

		Transform[] allTransforms = GetComponentsInChildren<Transform>(true);
		for (int i = 0; i < allTransforms.Length; i++)
		{
			if (allTransforms[i].name.StartsWith("Bone.") && !allTransforms[i].name.EndsWith("_end"))
			{
				blocks.Add(allTransforms[i]);
				blocksPoses.Add(allTransforms[i].localPosition);
			}
		}

		if (Progress.Instance != null && blocks.Count > 0)
		{
			Progress.Instance.SetMaxCount(blocks.Count);
		}
	}

	public void ResetMap()
	{
		if (blocks != null && blocksPoses != null && blocks.Count == blocksPoses.Count)
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (blocks[i] != null)
				{
					blocks[i].localPosition = blocksPoses[i];
					blocks[i].localScale = Vector3.one;
					Collider col = blocks[i].GetComponent<Collider>();
					if (col != null) col.enabled = true;
				}
			}
		}
		if (VoxelWallConverter.Instance != null)
		{
			VoxelWallConverter.Instance.ResetBlocks();
		}
		if (Progress.Instance != null)
		{
			Progress.Instance.InitializeLevelBaseline();
		}
	}
}
