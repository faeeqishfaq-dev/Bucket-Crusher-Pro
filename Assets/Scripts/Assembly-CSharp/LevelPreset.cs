using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class LevelPreset : ScriptableObject
{
	public LevelPosType LevelPosType;

	public Vector3 LevelPosition;

	public Texture2D Sprite;

	public Material Material;

	public Material OptimizedMaterial;

	public List<float> DifficultyPoints;
}
