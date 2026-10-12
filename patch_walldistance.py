import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

var_code = """	[Header("Draw My level")]
	[SerializeField]
	private LevelPreset _DrawMyLevelPreset;

	[Header("Distance Tuning")]
	[SerializeField]
	public float wallDistanceOffset = 0.5f;"""
text = text.replace('	[Header("Draw My level")]\n	[SerializeField]\n	private LevelPreset _DrawMyLevelPreset;', var_code)

startpos_code = """		Vector3 startPos = new Vector3(wallDistanceOffset, 0f, 0f);"""
text = text.replace('		Vector3 startPos = Vector3.zero;', startpos_code)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Updated MapCreatorEditor.cs with wallDistanceOffset")
