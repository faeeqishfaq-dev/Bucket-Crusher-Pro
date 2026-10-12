import sys

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'r', encoding='utf-8') as f:
    text = f.read()

import re

# Add fuelConsumptionRate field
text = re.sub(r'public class Oil : MonoBehaviour\s*\{', 'public class Oil : MonoBehaviour\n{\n\t[Header("Custom Settings")]\n\tpublic float fuelConsumptionRate = 15f;', text)

new_update_values = """	public void UpdateValues()
	{
		int oilUpgrade = PlayerPrefs.GetInt("OilUpgrade", 0);
		MaxOil = 50f + oilUpgrade * 25f;

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

		CurrentOilValue = Mathf.Clamp(CurrentOilValue, 0f, MaxOil);
		PlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);

		if (_fillImage != null && MaxOil > 0f)
		{
			_fillImage.fillAmount = CurrentOilValue / MaxOil;
		}
		if (_lowOil != null)
		{
			_lowOil.SetActive((CurrentOilValue / MaxOil) < 0.2f);
		}
	}"""

start_idx = text.find('public void UpdateValues()')
end_idx = text.find('public void StopSpend()')

if start_idx != -1 and end_idx != -1:
    text = text[:start_idx] + new_update_values + '\n\n\t' + text[end_idx:]

text = text.replace('CurrentOilValue -= Time.deltaTime * 5f;', 'CurrentOilValue -= Time.deltaTime * fuelConsumptionRate;\n\t\t\tPlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);')
text = text.replace('CurrentOilValue = 0f;\n\t\t\t\t_spendingOil = false;', 'CurrentOilValue = 0f;\n\t\t\t\tPlayerPrefs.SetFloat("SavedOilValue", 0f);\n\t\t\t\t_spendingOil = false;')

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print("Patched successfully")
