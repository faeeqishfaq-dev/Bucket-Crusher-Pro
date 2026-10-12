import re

# 1. Update Head.cs
head_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Head.cs'
with open(head_file, 'r', encoding='utf-8') as f:
    text = f.read()

# Add Crushing Power logic
crushing_logic = """	private float currentCrushingPower = -1f;

	public void UpdateCrushingPower()
	{
		int sizePurchases = PlayerPrefs.GetInt("SizeUpgrade", 0);
		int powerPurchases = PlayerPrefs.GetInt("PowerUpgrade", 0);
		
		float power = 0.10f + (sizePurchases + powerPurchases) * 0.05f;
		currentCrushingPower = Mathf.Clamp(power, 0.10f, 1.0f);
	}

	private float GetCrushingPower()
	{
		if (currentCrushingPower < 0f)
		{
			UpdateCrushingPower();
		}
		return currentCrushingPower;
	}

	void Trigged(Collider other)"""
text = text.replace('	void Trigged(Collider other)', crushing_logic)

# Use it in Trigged
trigged_repl = """		// Optimize: Use layer 10 which is exclusively for uncrushed wall bones
		if (other.gameObject.layer == 10)
		{
			// CRUSHING POWER PROGRESSION
			if (UnityEngine.Random.value > GetCrushingPower())
			{
				return; // Fail to crush this frame, simulates weaker/slower grinding
			}

			AddPhysics(other);"""
text = text.replace("""		// Optimize: Use layer 10 which is exclusively for uncrushed wall bones
		if (other.gameObject.layer == 10)
		{
			AddPhysics(other);""", trigged_repl)

with open(head_file, 'w', encoding='utf-8') as f:
    f.write(text)

# 2. Update Upgrade.cs
upgrade_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Upgrade.cs'
with open(upgrade_file, 'r', encoding='utf-8') as f:
    text2 = f.read()

size_repl = """        h.transform.localScale =
            Vector3.one * scale;
        h.UpdateCrushingPower();"""
text2 = text2.replace("""        h.transform.localScale =
            Vector3.one * scale;""", size_repl)

power_repl = """        if (h != null)
        {
            h.UpdateRotationSpeed();
            h.UpdateCrushingPower();
        }"""
text2 = text2.replace("""        if (h != null)
        {
            h.UpdateRotationSpeed();
        }""", power_repl)

with open(upgrade_file, 'w', encoding='utf-8') as f:
    f.write(text2)

print('Patched Head.cs and Upgrade.cs')
