import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Revert the variable addition
var_code = """	[Header("Draw My level")]
	[SerializeField]
	private LevelPreset _DrawMyLevelPreset;

	[Header("Distance Tuning")]
	[SerializeField]
	public float wallDistanceOffset = 0.5f;"""
original_var = """	[Header("Draw My level")]
	[SerializeField]
	private LevelPreset _DrawMyLevelPreset;"""

text = text.replace(var_code, original_var)

# Revert the startPos change
startpos_code = """		Vector3 startPos = new Vector3(wallDistanceOffset, 0f, 0f);"""
original_startpos = """		Vector3 startPos = Vector3.zero;"""

text = text.replace(startpos_code, original_startpos)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Reverted MapCreatorEditor.cs")
