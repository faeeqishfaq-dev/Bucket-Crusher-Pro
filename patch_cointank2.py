import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/CoinTankTrigger.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """using UnityEngine;
using TMPro;

public class CoinTankTrigger : MonoBehaviour
{
    private GameProperties gp;
    private TextMeshPro multiplierText;
    private TextMeshProUGUI multiplierTextUI;
    
    private float cachedMultiplier = 1f;
    private string lastText = "";
    private float checkTimer = 0f;

    private void Start()
    {
        gp = Object.FindAnyObjectByType<GameProperties>();
        
        GameObject multObj = GameObject.Find("MultiplierText");
        if (multObj != null)
        {
            multiplierText = multObj.GetComponent<TextMeshPro>();
            if (multiplierText == null) multiplierText = multObj.GetComponentInChildren<TextMeshPro>();
            
            multiplierTextUI = multObj.GetComponent<TextMeshProUGUI>();
            if (multiplierTextUI == null) multiplierTextUI = multObj.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > 0.5f) // Update multiplier every 0.5s instead of every coin
        {
            checkTimer = 0f;
            string txt = "";
            if (multiplierText != null) txt = multiplierText.text;
            else if (multiplierTextUI != null) txt = multiplierTextUI.text;

            if (txt != lastText && !string.IsNullOrEmpty(txt))
            {
                lastText = txt;
                string cleanTxt = txt.ToLower().Replace("x", "").Trim();
                if (float.TryParse(cleanTxt, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float mult))
                {
                    cachedMultiplier = mult;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
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
                if (gp != null)
                {
                    gp.SetMoney(cachedMultiplier);
                }
            }
        }
    }
}

public class CoinData : MonoBehaviour
{
    public bool collected = false;
}"""

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(optimized)

print("Optimized CoinTankTrigger.cs (full)")
