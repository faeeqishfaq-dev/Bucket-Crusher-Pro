import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """	private bool _joystickSearched = false;

	private void Update()
	{
		if (!_spendingOil) return;

		if (Joystick == null && !_joystickSearched)
		{
			Joystick = UnityEngine.Object.FindAnyObjectByType<DynamicJoystick>();
			if (Joystick == null) Joystick = UnityEngine.Object.FindAnyObjectByType<Joystick>();
			_joystickSearched = true;
		}"""

text = re.sub(r'void Update\(\)[\s\S]*?if \(Joystick == null\) Joystick = UnityEngine\.Object\.FindAnyObjectByType<Joystick>\(\);\s*\}', optimized, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Optimized Oil.cs")
