import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

original = """	private void Update()
	{
		if (!_spendingOil) return;

		if (Joystick == null)
		{
			Joystick = UnityEngine.Object.FindAnyObjectByType<DynamicJoystick>();
			if (Joystick == null) Joystick = UnityEngine.Object.FindAnyObjectByType<Joystick>();
		}"""

text = re.sub(r'private bool _joystickSearched = false;\s*private void Update\(\)[\s\S]*?_joystickSearched = true;\s*\}', original, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Reverted Oil.cs')
