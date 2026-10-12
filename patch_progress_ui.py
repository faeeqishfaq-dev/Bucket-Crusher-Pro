import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Progress.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """	private bool _isUiDirty = false;
	private int _lastDisplayedPct = -1;

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
				int intPct = Mathf.RoundToInt(pct * 100f);
				if (intPct != _lastDisplayedPct)
				{
					_textProgress.text = intPct + "%";
					_lastDisplayedPct = intPct;
				}
			}
			_isUiDirty = false;
		}
	}"""

text = re.sub(r'private bool _isUiDirty = false;\s*private void Update\(\)[\s\S]*?_isUiDirty = false;\s*\}\s*\}', optimized, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Optimized Progress.cs")
