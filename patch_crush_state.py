import re

oil_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs'
with open(oil_file, 'r', encoding='utf-8') as f:
    text = f.read()

if 'public bool IsSpendingOil' not in text:
    text = text.replace('private bool _spendingOil;', 'private bool _spendingOil;\n\n\tpublic bool IsSpendingOil => _spendingOil;')

with open(oil_file, 'w', encoding='utf-8') as f:
    f.write(text)

head_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Head.cs'
with open(head_file, 'r', encoding='utf-8') as f:
    text2 = f.read()

# Add check to Trigged
trigger_code = """	void Trigged(Collider other)
	{
		if (other == null || !other.enabled) return;

		// Check if game is in a valid playing state (Oil spending is active)
		if (GameManager.Instance != null && GameManager.Instance.currentOilUI != null)
		{
			if (!GameManager.Instance.currentOilUI.IsSpendingOil) return;
		}

		// Optimize: Use layer 10 which is exclusively for uncrushed wall bones"""

text2 = text2.replace("""	void Trigged(Collider other)
	{
		if (other == null || !other.enabled) return;

		// Optimize: Use layer 10 which is exclusively for uncrushed wall bones""", trigger_code)

with open(head_file, 'w', encoding='utf-8') as f:
    f.write(text2)

print('Patched Head.cs and Oil.cs')
