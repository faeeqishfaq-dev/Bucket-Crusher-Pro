import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/CoinTankTrigger.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CollectedCoin")
        {
            CoinData data = other.GetComponent<CoinData>();
            if (data == null)
            {
                data = other.gameObject.AddComponent<CoinData>();
            }

            if (!data.collected)
            {
                data.collected = true;
                
                float amount = 1f;
                string txt = "";
                if (multiplierText != null) txt = multiplierText.text;
                else if (multiplierTextUI != null) txt = multiplierTextUI.text;

                if (!string.IsNullOrEmpty(txt))
                {
                    txt = txt.ToLower().Replace("x", "").Trim();
                    if (float.TryParse(txt, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float mult))
                    {
                        amount = mult;
                    }
                }
                
                if (gp != null)
                {
                    gp.SetMoney(amount);
                }
            }
        }
    }"""

text = re.sub(r'private void OnTriggerEnter[\s\S]*?gp\.SetMoney\(amount\);\s*\}\s*\}\s*\}', optimized, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Optimized CoinTankTrigger.cs")
