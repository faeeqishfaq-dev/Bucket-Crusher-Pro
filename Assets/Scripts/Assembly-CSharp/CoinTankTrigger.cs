using UnityEngine;
using TMPro;

public class CoinTankTrigger : MonoBehaviour
{
    private GameProperties gp;
    private TextMeshPro multiplierText;
    private TextMeshProUGUI multiplierTextUI;

    private void Start()
    {
        gp = Object.FindAnyObjectByType<GameProperties>();
        
        // Find MultiplierText
        GameObject multObj = GameObject.Find("MultiplierText");
        if (multObj != null)
        {
            multiplierText = multObj.GetComponent<TextMeshPro>();
            if (multiplierText == null) multiplierText = multObj.GetComponentInChildren<TextMeshPro>();
            
            multiplierTextUI = multObj.GetComponent<TextMeshProUGUI>();
            if (multiplierTextUI == null) multiplierTextUI = multObj.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CollectedCoin")
        {
            // Verify it hasn't been collected yet
            CoinData data = other.GetComponent<CoinData>();
            if (data == null)
            {
                data = other.gameObject.AddComponent<CoinData>();
                data.collected = true;
                
                // Calculate amount based on multiplier
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
    }
}

public class CoinData : MonoBehaviour
{
    public bool collected = false;
}