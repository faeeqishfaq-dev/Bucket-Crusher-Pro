using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MapCreatorEditor : MonoBehaviour
{
	[SerializeField]
	private Map _map;

	[SerializeField]
	private LevelPreset[] _levelPresets;

	[SerializeField]
	private Mesh _simpleMesh;

	[SerializeField]
	private SkinnedMeshRenderer _simpleMeshRen;

	[SerializeField]
	private Transform _parent;

	[Header("HouseFirst ab levels")]
	[SerializeField]
	private LevelPreset[] _levelPresetsAB;

	[Header("startPos ab")]
	[SerializeField]
	private LevelPreset[] _levelPresetsStartPosAB;

	[Header("RailsDigging ab")]
	[SerializeField]
	private LevelPreset[] _levelPresetsRailsAB;

	[Header("Bonus level")]
	[SerializeField]
	private LevelPreset _bonuslevelPreset;

	[Header("Bonus level")]
	[SerializeField]
	private LevelPreset _minecraftlevelPreset;

	[Header("Draw My level")]
	[SerializeField]
	private LevelPreset _DrawMyLevelPreset;

	private List<Vector2> trashHolder;

	private float X;

	private float Y;

	private int _level;

	private Texture2D _currentSprite;

	private List<Vector3> blocksPoses;

	public LevelPosType levelPosType;

	public LevelPreset CurrentLevel;

	private Vector2Int m_GeneratedLevelSize;

	public Map Map => _map;

	public bool IsBonusLevel { get; private set; }

	private void Start()
	{
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		_level = gp != null ? gp.Level() : 1;

		if (_levelPresets != null && _levelPresets.Length > 0)
		{
			int idx = (_level - 1) % _levelPresets.Length;
			CurrentLevel = _levelPresets[idx];
		}

		if (CurrentLevel != null)
		{
			levelPosType = CurrentLevel.LevelPosType;
			if (_simpleMeshRen != null && CurrentLevel.Material != null)
			{
				_simpleMeshRen.material = CurrentLevel.Material;
			}
		}

		if (_map != null)
		{
			GenerateLevel();
		}
	}

	private bool IsDrawing()
	{
		return false;
	}

	public void GenerateLevel()
	{
		if (CurrentLevel == null || CurrentLevel.Sprite == null || _map == null)
		{
			_map?.Processs();
			return;
		}

		_currentSprite = CurrentLevel.Sprite;

		// 1. Destroy old blocks (keep the original prefab "Bone")
		Transform[] allTransforms = _map.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in allTransforms)
		{
			if (t != null && t != _map.transform && t.name.StartsWith("Bone.") && !t.name.EndsWith("_end"))
			{
				t.parent = null;
				Destroy(t.gameObject);
			}
		}

		// 2. Find prefab
		GameObject bonePrefab = null;
		foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
		{
			if (go.hideFlags == HideFlags.None && go.name == "Bone")
			{
				bonePrefab = go;
				break;
			}
		}
		if (bonePrefab == null)
		{
			_map.Processs();
			return;
		}

		// 3. Generate from Sprite
		Color[] pixels = _currentSprite.GetPixels();
		int width = _currentSprite.width;
		int height = _currentSprite.height;

		Vector3 startPos = Vector3.zero;
		int step = 7;
		float spacingX = 0.077f / 100f;
		float spacingY = 0.055f / 100f;

		// Calculate offsets so the wall is centered horizontally around LevelPosition
		float offsetX = 0f;
		
		for (int y = 0; y < height; y += step)
		{
			for (int x = 0; x < width; x += step)
			{
				Color c = pixels[y * width + x];
				if (c.a > 0.1f) // Pixel is visible
				{
					GameObject newBone = Instantiate(bonePrefab, _map.transform);
					newBone.name = "Bone." + x + "." + y;
					newBone.transform.localPosition = startPos + new Vector3(-((float)x / step) * spacingX, ((float)y / step) * spacingY, 0f);
					newBone.SetActive(true);
				}
			}
		}

		_map.Processs();
		VoxelWallConverter converter = _map.GetComponent<VoxelWallConverter>();
		if (converter != null)
		{
			converter.ResetBlocks();
		}

		if (Progress.Instance != null)
		{
			Progress.Instance.InitializeLevelBaseline();
		}
	}
}
