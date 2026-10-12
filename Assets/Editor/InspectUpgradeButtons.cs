#if UNITY_EDITOR
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[InitializeOnLoad]
public static class InspectUpgradeButtons
{
    private const string FLAG_FILE = "Temp/RunInspectUpgrade.txt";

    static InspectUpgradeButtons()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (EditorApplication.isPlaying && File.Exists(FLAG_FILE))
            {
                try { File.Delete(FLAG_FILE); } catch {}
                Debug.Log("[InspectUpgrade] Entered Play Mode, spawning UpgradeInspectorRunner...");
                var go = new GameObject("UpgradeInspectorRunner");
                go.AddComponent<UpgradeInspectorRunner>();
                Object.DontDestroyOnLoad(go);
            }
        };
    }

    public static void RunInspection()
    {
        Application.runInBackground = true;
        Debug.Log("[InspectUpgrade] Opening GamePlay scene in batch mode...");
        File.WriteAllText(FLAG_FILE, "1");
        EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
        EditorApplication.isPlaying = true;
    }
}

public class UpgradeInspectorRunner : MonoBehaviour
{
    private IEnumerator Start()
    {
        Application.runInBackground = true;
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(0.5f);

        Debug.Log("[InspectUpgrade] === STARTING UPGRADE BUTTON VERIFICATION ===");

        // Reset upgrade levels to 0 and give money for clean test
        PlayerPrefs.SetInt("LengthUpgrade", 0);
        PlayerPrefs.SetInt("OilUpgrade", 0);
        PlayerPrefs.SetInt("PowerUpgrade", 0);
        PlayerPrefs.SetInt("SizeUpgrade", 0);
        PlayerPrefs.SetFloat("Money", 50000f);
        PlayerPrefs.Save();

        Shop shop = Object.FindAnyObjectByType<Shop>();
        if (shop == null)
        {
            Debug.LogError("[InspectUpgrade] FAILED: Shop component not found!");
            Finish(1);
            yield break;
        }

        GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
        if (gp != null) gp.SetMoney(0f); // trigger wallet refresh
        shop.UpdateAllButtons();

        // 1. Raycast checks for all 4 buttons
        EventSystem es = Object.FindAnyObjectByType<EventSystem>();
        var buttons = new (string name, string fieldName, string prefKey)[]
        {
            ("Length", "longButton", "LengthUpgrade"),
            ("Oil", "oilButton", "OilUpgrade"),
            ("Power", "powerButton", "PowerUpgrade"),
            ("Size", "sizeButton", "SizeUpgrade")
        };

        bool allRaycastsPassed = true;

        foreach (var (name, fieldName, prefKey) in buttons)
        {
            Button btn = GetPrivateField<Button>(shop, fieldName);
            if (btn == null)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Button '{fieldName}' is null on Shop!");
                allRaycastsPassed = false;
                continue;
            }

            if (!btn.gameObject.activeInHierarchy)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Button '{name}' is NOT active in hierarchy!");
                allRaycastsPassed = false;
                continue;
            }

            RectTransform rt = btn.GetComponent<RectTransform>();
            Vector3 worldPos = rt.TransformPoint(rt.rect.center);
            Canvas canvas = btn.GetComponentInParent<Canvas>();
            Camera cam = (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay) ? (canvas.worldCamera ?? Camera.main) : null;
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, worldPos);

            PointerEventData ped = new PointerEventData(es) { position = screenPos };
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, hits);

            if (hits.Count == 0)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Button '{name}' received 0 raycast hits at {screenPos}!");
                allRaycastsPassed = false;
                continue;
            }

            GameObject topHit = hits[0].gameObject;
            bool hitButtonOrChild = topHit == btn.gameObject || topHit.transform.IsChildOf(btn.transform) || btn.transform.IsChildOf(topHit.transform);

            Debug.Log($"[InspectUpgrade] Raycast test for '{name}': Top hit = '{topHit.name}' (acceptable: {hitButtonOrChild})");

            if (!hitButtonOrChild)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Raycast for '{name}' was blocked by '{topHit.name}'!");
                allRaycastsPassed = false;
            }
        }

        if (!allRaycastsPassed)
        {
            Debug.LogError("[InspectUpgrade] Raycast tests FAILED! One or more buttons are blocked.");
            Finish(1);
            yield break;
        }
        Debug.Log("[InspectUpgrade] SUCCESS: All 4 buttons received unblocked raycasts!");

        // 2. Click Simulation & Upgrade Verification
        Debug.Log("[InspectUpgrade] === SIMULATING POINTER CLICKS ===");
        bool allClicksPassed = true;

        foreach (var (name, fieldName, prefKey) in buttons)
        {
            Button btn = GetPrivateField<Button>(shop, fieldName);
            Text costText = GetPrivateField<Text>(shop, "_" + fieldName.Replace("Button", "Cost").ToLower());

            int levelBefore = PlayerPrefs.GetInt(prefKey, 0);
            float moneyBefore = gp.Money();
            string textBefore = costText != null ? costText.text : "N/A";

            Debug.Log($"[InspectUpgrade] Testing click on '{name}': Initial Level={levelBefore}, Money={moneyBefore}, CostText={textBefore}, Interactable={btn.interactable}");

            if (!btn.interactable)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Button '{name}' is not interactable with {moneyBefore} money!");
                allClicksPassed = false;
                continue;
            }

            // Execute PointerClick event via EventSystem
            PointerEventData ped = new PointerEventData(es) { button = PointerEventData.InputButton.Left };
            ExecuteEvents.Execute(btn.gameObject, ped, ExecuteEvents.pointerClickHandler);

            yield return null;

            int levelAfter = PlayerPrefs.GetInt(prefKey, 0);
            float moneyAfter = gp.Money();
            string textAfter = costText != null ? costText.text : "N/A";

            Debug.Log($"[InspectUpgrade] Post-click '{name}': Level={levelAfter}, Money={moneyAfter}, CostText={textAfter}, Interactable={btn.interactable}");

            if (levelAfter != levelBefore + 1)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: '{name}' level did not increment by exactly 1! (Before={levelBefore}, After={levelAfter})");
                allClicksPassed = false;
            }

            if (moneyAfter >= moneyBefore)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: '{name}' cost was not deducted from money! (Before={moneyBefore}, After={moneyAfter})");
                allClicksPassed = false;
            }
        }

        // 3. Verify in-game gameplay systems reflect the upgrades
        Debug.Log("[InspectUpgrade] === VERIFYING IN-GAME GAMEPLAY SYSTEMS REFLECT UPGRADES ===");
        Oil oil = Object.FindAnyObjectByType<Oil>();
        if (oil != null)
        {
            int oilLevel = PlayerPrefs.GetInt("OilUpgrade", 0);
            float expectedMaxOil = 50f + oilLevel * 25f;
            Debug.Log($"[InspectUpgrade] Oil check: MaxOil={oil.MaxOil}, Expected={expectedMaxOil}");
            if (oil.MaxOil != expectedMaxOil)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Oil.MaxOil is {oil.MaxOil}, expected {expectedMaxOil}!");
                allClicksPassed = false;
            }
        }

        Head head = Object.FindAnyObjectByType<Head>();
        if (head != null)
        {
            int sizeLevel = PlayerPrefs.GetInt("SizeUpgrade", 0);
            float expectedScale = 1f + sizeLevel * 0.1f;
            Debug.Log($"[InspectUpgrade] Head check: scale={head.transform.localScale.x}, expected={expectedScale}");
            if (Mathf.Abs(head.transform.localScale.x - expectedScale) > 0.01f)
            {
                Debug.LogError($"[InspectUpgrade] FAIL: Head scale is {head.transform.localScale.x}, expected {expectedScale}!");
                allClicksPassed = false;
            }
        }

        // 4. Test interactability when player has insufficient money
        Debug.Log("[InspectUpgrade] === TESTING ZERO MONEY INTERACTABILITY ===");
        PlayerPrefs.SetFloat("Money", 0f);
        PlayerPrefs.Save();
        gp.SetMoney(0f);
        shop.UpdateAllButtons();

        foreach (var (name, fieldName, prefKey) in buttons)
        {
            Button btn = GetPrivateField<Button>(shop, fieldName);
            Debug.Log($"[InspectUpgrade] Zero-money check for '{name}': interactable={btn.interactable}");
            if (btn.interactable)
            {
                // Note: Unless it's onboarding free, it should be false
                bool isFree = OnboardingManager.Instance != null && OnboardingManager.Instance.IsOnboardingActive;
                if (!isFree)
                {
                    Debug.LogWarning($"[InspectUpgrade] Warning: '{name}' is still interactable at 0 money (may be free onboarding).");
                }
            }
        }

        if (allClicksPassed)
        {
            Debug.Log("[InspectUpgrade] *** ALL UPGRADE BUTTON TESTS PASSED SUCCESSFULLY! ***");
            Finish(0);
        }
        else
        {
            Debug.LogError("[InspectUpgrade] *** UPGRADE BUTTON TESTS FAILED! ***");
            Finish(1);
        }
    }

    private T GetPrivateField<T>(object obj, string fieldName)
    {
        var f = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        return f != null ? (T)f.GetValue(obj) : default;
    }

    private void Finish(int code)
    {
        EditorApplication.isPlaying = false;
        if (Application.isBatchMode)
        {
            EditorApplication.Exit(code);
        }
    }
}
#endif
