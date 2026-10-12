import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/DynamicJoystick.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# I will replace the entire OnPointerDown method in DynamicJoystick.cs
# because I messed it up with UI guards earlier.

old_pd = re.search(r'public override void OnPointerDown.*?base\.OnPointerDown\(eventData\);\n\t}', text, re.DOTALL).group(0)

clean_pd = '''	public override void OnPointerDown(PointerEventData eventData)
	{
		if (isPointerDown) return;

		if (IsPointerOverMultiplierButton(eventData.position)) return;

		if (background != null)
		{
			background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
			background.gameObject.SetActive(true);
		}
		base.OnPointerDown(eventData);
	}'''

text = text.replace(old_pd, clean_pd)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('DynamicJoystick.cs patched.')
