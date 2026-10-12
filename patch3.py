import sys
import re

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'r', encoding='utf-8') as f:
    text = f.read()

# Add _isMaxed
text = text.replace('private bool _initialized = false;', 'private bool _isMaxed = false;\n\tprivate bool _initialized = false;')

# Replace UpdateValues
new_update_values = """	public void UpdateValues()
	{
		int oilUpgrade = PlayerPrefs.GetInt("OilUpgrade", 0);
		MaxOil = 50f + oilUpgrade * 25f;
		
		_isMaxed = MaxOilUpgrade != null && oilUpgrade >= MaxOilUpgrade.Max;

		if (!_initialized)
		{
			_initialized = true;
			lastUpgradeLevel = oilUpgrade;

			float savedOil = PlayerPrefs.GetFloat("SavedOilValue", -1f);
			int currentLvl = PlayerPrefs.GetInt("Level", 1);
			int hasPlayed = PlayerPrefs.GetInt("HasPlayedLvl1", 0);

			if (currentLvl <= 1 && hasPlayed == 0)
			{
				CurrentOilValue = MaxOil * 0.50f;
				PlayerPrefs.SetInt("HasPlayedLvl1", 1);
			}
			else if (savedOil >= 0f)
			{
				CurrentOilValue = savedOil;
			}
			else
			{
				CurrentOilValue = MaxOil * 0.50f;
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

		if (_isMaxed)
		{
			CurrentOilValue = MaxOil;
		}

		CurrentOilValue = Mathf.Clamp(CurrentOilValue, 0f, MaxOil);
		PlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);

		if (_fillImage != null && MaxOil > 0f)
		{
			_fillImage.fillAmount = CurrentOilValue / MaxOil;
		}
		if (_lowOil != null)
		{
			_lowOil.SetActive((CurrentOilValue / MaxOil) < 0.2f && !_isMaxed);
		}
	}"""

start_idx = text.find('public void UpdateValues()')
end_idx = text.find('public void StopSpend()')

if start_idx != -1 and end_idx != -1:
    text = text[:start_idx] + new_update_values + '\n\n\t' + text[end_idx:]

# Modify Update
text = text.replace('CurrentOilValue -= Time.deltaTime * fuelConsumptionRate;', 'if (!_isMaxed)\n\t\t\t{\n\t\t\t\tCurrentOilValue -= Time.deltaTime * fuelConsumptionRate;\n\t\t\t}')

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched Oil.cs for MAX progression successfully')
