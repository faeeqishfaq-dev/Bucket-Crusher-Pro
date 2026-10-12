import sys

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'r', encoding='utf-8') as f:
    lines = f.readlines()

new_update_values = """	public void UpdateValues()
	{
		int oilUpgrade = PlayerPrefs.GetInt("OilUpgrade", 0);
		MaxOil = 50f + oilUpgrade * 25f;

		if (!_initialized)
		{
			_initialized = true;
			lastUpgradeLevel = oilUpgrade;

			int currentLvl = PlayerPrefs.GetInt("Level", 1);
			if (currentLvl <= 1)
			{
				CurrentOilValue = MaxOil * 0.50f;
			}
			else
			{
				CurrentOilValue = MaxOil;
			}
		}
		else
		{
			if (oilUpgrade > lastUpgradeLevel)
			{
				int diff = oilUpgrade - lastUpgradeLevel;
				CurrentOilValue += MaxOil * 0.10f * diff;
				lastUpgradeLevel = oilUpgrade;
			}
		}

		CurrentOilValue = Mathf.Clamp(CurrentOilValue, 0f, MaxOil);

		if (_fillImage != null && MaxOil > 0f)
		{
			_fillImage.fillAmount = CurrentOilValue / MaxOil;
		}
		if (_lowOil != null)
		{
			_lowOil.SetActive((CurrentOilValue / MaxOil) < 0.2f);
		}
	}
"""

start_idx = -1
end_idx = -1
for i, line in enumerate(lines):
    if 'public void UpdateValues()' in line:
        start_idx = i
    if start_idx != -1 and i > start_idx and 'public void StopSpend()' in line:
        end_idx = i
        break

if start_idx != -1 and end_idx != -1:
    # Also replace 'private int lastUpgradeLevel;' with our new fields
    new_lines = lines[:start_idx] + [new_update_values, '\n'] + lines[end_idx:]
    
    # Do string replace for the fields
    content = "".join(new_lines)
    content = content.replace('private int lastUpgradeLevel;', 'private bool _initialized = false;\n\tprivate int lastUpgradeLevel = -1;')
    
    with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'w', encoding='utf-8') as f:
        f.write(content)
    print('Patched Oil.cs successfully')
else:
    print('Could not find UpdateValues boundaries')
