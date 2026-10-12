import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Progress.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

original = """	private bool _isUiDirty = false;
		private void Update()
	{
		if (_isUiDirty)
		{
			float pct = GetProgressPct();
			if (_slider != null)
			{
				_slider.value = pct;
			}
			if (_textProgress != null)
			{
				_textProgress.text = Mathf.RoundToInt(pct * 100f) + "%";
			}
			_isUiDirty = false;
		}
	}"""

text = re.sub(r'private bool _isUiDirty = false;\s*private int _lastDisplayedPct = -1;\s*private void Update\(\)[\s\S]*?_isUiDirty = false;\s*\}\s*\}', original, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Reverted Progress.cs')
