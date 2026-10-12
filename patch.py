import re

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Sell.cs', 'r', encoding='utf-8') as f:
    text = f.read()

new_method = '''    public void OnClickRV()
    {
        if (_MultiplierRV != null && !_MultiplierRV.activeInHierarchy) return;

        float reward = 10000f; 
        if (_MultiplierRVText != null && !string.IsNullOrEmpty(_MultiplierRVText.text))
        {
            string t = _MultiplierRVText.text.Replace("+", "").Replace("$", "").Replace(" ", "").Replace(",", "");
            float.TryParse(t, out reward);
        }
        
        GameProperties gp = UnityEngine.Object.FindAnyObjectByType<GameProperties>();
        if (gp != null)
        {
            gp.SetMoney(reward);
        }
        
        if (this.Money == null)
        {
            this.Money = UnityEngine.Object.FindAnyObjectByType<Money>();
        }
        if (this.Money != null)
        {
            this.Money.UpdateValue();
        }
        
        if (_MultiplierFX != null)
        {
            _MultiplierFX.SetActive(false);
            _MultiplierFX.SetActive(true);
            ParticleSystem[] ps = _MultiplierFX.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var p in ps)
            {
                p.Stop();
                p.Clear();
                p.Play();
            }
        }

        if (_MultiplierRV != null)
        {
            _MultiplierRV.SetActive(false);
        }
    }'''

text = re.sub(r'public void OnClickRV\(\)\s*\{[\s\S]*?\}', new_method, text)

with open('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Sell.cs', 'w', encoding='utf-8') as f:
    f.write(text)

print('Updated Sell.cs')
