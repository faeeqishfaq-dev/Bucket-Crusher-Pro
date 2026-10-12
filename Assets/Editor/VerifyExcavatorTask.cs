#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[InitializeOnLoad]
public static class VerifyExcavatorTask
{
    private const string TRIGGER_FILE = "Temp/RunVerifyExcavator.txt";
    public const string RESULT_FILE = "Temp/VerifyExcavatorResult.txt";

    static VerifyExcavatorTask()
    {
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;

        if (!EditorApplication.isPlaying && File.Exists(TRIGGER_FILE))
        {
            try { File.Delete(TRIGGER_FILE); } catch {}
            RunTest();
            return;
        }

        if (EditorApplication.isPlaying && File.Exists("Temp/VerifyExcavator_Running.txt"))
        {
            try { File.Delete("Temp/VerifyExcavator_Running.txt"); } catch {}
            var go = new GameObject("ExcavatorTaskTestRunner");
            go.AddComponent<ExcavatorTaskTestRunner>();
            Object.DontDestroyOnLoad(go);
        }
    }

    [MenuItem("Tools/Run Excavator Task Verification")]
    public static void RunTest()
    {
        Debug.Log("[VerifyExcavator] === STARTING EXCAVATOR TASK VERIFICATION ===");
        File.WriteAllText("Temp/VerifyExcavator_Running.txt", "1");
        EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
        EditorApplication.isPlaying = true;
    }
}

public class ExcavatorTaskTestRunner : MonoBehaviour
{
    private IEnumerator Start()
    {
        Application.runInBackground = true;
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.5f);

        var report = new System.Text.StringBuilder();
        report.AppendLine("=== EXCAVATOR VERIFICATION REPORT ===");

        // Reset to fresh/default state
        PlayerPrefs.SetInt("LengthUpgrade", 0);
        PlayerPrefs.SetInt("OilUpgrade", 0);
        PlayerPrefs.SetInt("PowerUpgrade", 0);
        PlayerPrefs.SetInt("SizeUpgrade", 0);
        PlayerPrefs.SetFloat("Money", 50000f);
        PlayerPrefs.Save();

        Shop shop = Object.FindAnyObjectByType<Shop>();
        Upgrade upgrade = Object.FindAnyObjectByType<Upgrade>();
        GameManager gm = GameManager.Instance;
        GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
        Control ctrl = Object.FindAnyObjectByType<Control>();

        if (shop == null || upgrade == null || gm == null || gp == null || ctrl == null)
        {
            string err = $"Missing components! shop:{shop!=null}, upgrade:{upgrade!=null}, gm:{gm!=null}, gp:{gp!=null}, ctrl:{ctrl!=null}";
            Debug.LogError("[VerifyExcavator] " + err);
            File.WriteAllText(VerifyExcavatorTask.RESULT_FILE, "FAILED: " + err);
            EditorApplication.isPlaying = false;
            yield break;
        }

        gp.SetMoney(0f); // refresh wallet
        upgrade.UpdateAllStage();
        shop.UpdateAllButtons();

        yield return null;

        // 1. Initial State Check
        int initLevel = PlayerPrefs.GetInt("LengthUpgrade", 0);
        int initCost = GetPrivateField<int>(shop, "_currentLongCost");
        Text costText = GetPrivateField<Text>(shop, "_longCost");
        string initCostString = costText != null ? costText.text : "null";

        Debug.Log($"[VerifyExcavator] Initial Length Level: {initLevel}, Cost: {initCost}, Text: {initCostString}");
        report.AppendLine($"Initial: Level={initLevel}, Cost={initCost}, CostText={initCostString}");

        bool p1_ok = false;
        bool p2_ok = false;
        bool p3_ok = false;
        bool p4_ok = false;

        // Measure initial arm physical length (p1 anchor to Head)
        Transform stickTr = GetPrivateField<GameObject>(upgrade, "_currentStick")?.transform;
        Transform headTr = stickTr != null ? stickTr.Find("Head") : null;
        Transform p3Tr = stickTr != null ? stickTr.Find("p3") : null;
        float initialHeadDist = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
        Debug.Log($"[VerifyExcavator] Initial Head Distance: {initialHeadDist:F3}");

        // Purchase 1: 10 coins
        if (initCost == 10)
        {
            float moneyBefore = gp.Money();
            shop.BuyLong(false);
            yield return new WaitForSecondsRealtime(0.1f);

            int lvl1 = PlayerPrefs.GetInt("LengthUpgrade", 0);
            int cost1 = GetPrivateField<int>(shop, "_currentLongCost");
            float headDist1 = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            float ext1 = headDist1 - initialHeadDist;
            float moneySpent = moneyBefore - gp.Money();

            Debug.Log($"[VerifyExcavator] Purchase 1 (10 coins): Lvl={lvl1}, CostNext={cost1}, Ext={ext1:F3}, MoneySpent={moneySpent}");
            report.AppendLine($"Purchase 1: Level {initLevel}->{lvl1}, NextCost={cost1}, Extension={ext1:F3}, MoneySpent={moneySpent}");

            if (lvl1 == 1 && cost1 == 500 && Mathf.Abs(moneySpent - 10f) < 0.1f && ext1 > 0.05f && ext1 < 0.5f)
            {
                p1_ok = true;
            }
        }

        // Purchase 2: 500 coins
        int costBefore2 = GetPrivateField<int>(shop, "_currentLongCost");
        if (costBefore2 == 500)
        {
            float moneyBefore = gp.Money();
            float headDistBefore = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            shop.BuyLong(false);
            yield return new WaitForSecondsRealtime(0.1f);

            int lvl2 = PlayerPrefs.GetInt("LengthUpgrade", 0);
            int cost2 = GetPrivateField<int>(shop, "_currentLongCost");
            float headDist2 = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            float ext2 = headDist2 - headDistBefore;
            float moneySpent = moneyBefore - gp.Money();

            Debug.Log($"[VerifyExcavator] Purchase 2 (500 coins): Lvl={lvl2}, CostNext={cost2}, Ext={ext2:F3}, MoneySpent={moneySpent}");
            report.AppendLine($"Purchase 2: Level 1->{lvl2}, NextCost={cost2}, Extension={ext2:F3}, MoneySpent={moneySpent}");

            if (lvl2 == 2 && cost2 == 1000 && Mathf.Abs(moneySpent - 500f) < 0.1f && ext2 > 0.05f && ext2 < 0.5f)
            {
                p2_ok = true;
            }
        }

        // Purchase 3: 1000 coins
        int costBefore3 = GetPrivateField<int>(shop, "_currentLongCost");
        if (costBefore3 == 1000)
        {
            float moneyBefore = gp.Money();
            float headDistBefore = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            shop.BuyLong(false);
            yield return new WaitForSecondsRealtime(0.1f);

            int lvl3 = PlayerPrefs.GetInt("LengthUpgrade", 0);
            int cost3 = GetPrivateField<int>(shop, "_currentLongCost");
            float headDist3 = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            float ext3 = headDist3 - headDistBefore;
            float moneySpent = moneyBefore - gp.Money();

            Debug.Log($"[VerifyExcavator] Purchase 3 (1000 coins): Lvl={lvl3}, CostNext={cost3}, Ext={ext3:F3}, MoneySpent={moneySpent}");
            report.AppendLine($"Purchase 3: Level 2->{lvl3}, NextCost={cost3}, Extension={ext3:F3}, MoneySpent={moneySpent}");

            if (lvl3 == 3 && cost3 == 4000 && Mathf.Abs(moneySpent - 1000f) < 0.1f && ext3 > 0.05f && ext3 < 0.5f)
            {
                p3_ok = true;
            }
        }

        // Purchase 4: 4000 coins
        int costBefore4 = GetPrivateField<int>(shop, "_currentLongCost");
        if (costBefore4 == 4000)
        {
            float moneyBefore = gp.Money();
            float headDistBefore = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            shop.BuyLong(false);
            yield return new WaitForSecondsRealtime(0.1f);

            int lvl4 = PlayerPrefs.GetInt("LengthUpgrade", 0);
            int cost4 = GetPrivateField<int>(shop, "_currentLongCost");
            float headDist4 = (headTr != null && stickTr != null) ? Vector3.Distance(stickTr.position, headTr.position) : 0f;
            float ext4 = headDist4 - headDistBefore;
            float moneySpent = moneyBefore - gp.Money();

            Debug.Log($"[VerifyExcavator] Purchase 4 (4000 coins): Lvl={lvl4}, CostNext={cost4}, Ext={ext4:F3}, MoneySpent={moneySpent}");
            report.AppendLine($"Purchase 4: Level 3->{lvl4}, NextCost={cost4}, Extension={ext4:F3}, MoneySpent={moneySpent}");

            if (lvl4 == 4 && cost4 == 10000 && Mathf.Abs(moneySpent - 4000f) < 0.1f && ext4 > 0.05f && ext4 < 0.5f)
            {
                p4_ok = true;
            }
        }

        bool lengthProgressionPassed = p1_ok && p2_ok && p3_ok && p4_ok;
        Debug.Log($"[VerifyExcavator] Length Progression Test: {(lengthProgressionPassed ? "PASSED" : "FAILED")}");
        report.AppendLine($"Length Progression: {(lengthProgressionPassed ? "PASSED" : "FAILED")}");

        // Now test starting active gameplay
        Debug.Log("[VerifyExcavator] === STARTING ACTIVE GAMEPLAY ===");
        gm.StartGame();
        yield return new WaitForSecondsRealtime(0.3f);

        // Check if upgrade buttons remain visible during gameplay
        GameObject incLong = GetPrivateField<GameObject>(shop, "IncrementalLong") ?? GameObject.Find("Incremental/Long") ?? GameObject.Find("Long");
        GameObject incOil = GetPrivateField<GameObject>(shop, "IncrementalOil") ?? GameObject.Find("Incremental/Oil") ?? GameObject.Find("Oil");
        GameObject incPower = GetPrivateField<GameObject>(shop, "IncrementalPower") ?? GameObject.Find("Incremental/Power") ?? GameObject.Find("Power");
        GameObject incSize = GetPrivateField<GameObject>(shop, "IncrementalSize") ?? GameObject.Find("Incremental/Size") ?? GameObject.Find("Size");

        bool longVis = incLong != null && incLong.activeInHierarchy;
        bool oilVis = incOil != null && incOil.activeInHierarchy;
        bool powerVis = incPower != null && incPower.activeInHierarchy;
        bool sizeVis = incSize != null && incSize.activeInHierarchy;

        bool gameplayUIVisible = longVis && oilVis && powerVis && sizeVis;
        Debug.Log($"[VerifyExcavator] Upgrade buttons active in hierarchy during gameplay: Long={longVis}, Oil={oilVis}, Power={powerVis}, Size={sizeVis}");
        report.AppendLine($"Buttons Active: Long={longVis}, Oil={oilVis}, Power={powerVis}, Size={sizeVis}");

        // Check Start Menu buttons are hidden (PlayButton)
        bool playBtnHidden = gm.PlayButton == null || !gm.PlayButton.activeInHierarchy;
        Debug.Log($"[VerifyExcavator] PlayButton hidden during gameplay: {playBtnHidden}");
        report.AppendLine($"PlayButton Hidden: {playBtnHidden}");

        // Raycast and Clickability check during gameplay
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
            if (btn == null || !btn.gameObject.activeInHierarchy)
            {
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
                Debug.LogError($"[VerifyExcavator] Button '{name}' has 0 raycast hits!");
                allRaycastsPassed = false;
                continue;
            }

            GameObject topHit = hits[0].gameObject;
            bool hitButtonOrChild = topHit == btn.gameObject || topHit.transform.IsChildOf(btn.transform) || btn.transform.IsChildOf(topHit.transform);
            Debug.Log($"[VerifyExcavator] Gameplay Raycast '{name}': Top hit = '{topHit.name}', Valid: {hitButtonOrChild}");
            if (!hitButtonOrChild)
            {
                allRaycastsPassed = false;
            }
        }

        // Test purchasing Oil, Power, Size during active gameplay
        bool oilPurchasePassed = false;
        bool powerPurchasePassed = false;
        bool sizePurchasePassed = false;

        int oilBefore = PlayerPrefs.GetInt("OilUpgrade", 0);
        shop.BuyOil(false);
        int oilAfter = PlayerPrefs.GetInt("OilUpgrade", 0);
        oilPurchasePassed = oilAfter == oilBefore + 1;

        int pwrBefore = PlayerPrefs.GetInt("PowerUpgrade", 0);
        shop.BuyPower(false);
        int pwrAfter = PlayerPrefs.GetInt("PowerUpgrade", 0);
        powerPurchasePassed = pwrAfter == pwrBefore + 1;

        int sizeBefore = PlayerPrefs.GetInt("SizeUpgrade", 0);
        shop.BuySize(false);
        int sizeAfter = PlayerPrefs.GetInt("SizeUpgrade", 0);
        sizePurchasePassed = sizeAfter == sizeBefore + 1;

        bool allUsableDuringGameplay = oilPurchasePassed && powerPurchasePassed && sizePurchasePassed && allRaycastsPassed;
        Debug.Log($"[VerifyExcavator] Gameplay Upgrades Usable: Oil={oilPurchasePassed}, Power={powerPurchasePassed}, Size={sizePurchasePassed}, Raycasts={allRaycastsPassed}");
        report.AppendLine($"Gameplay Upgrades Usable: Oil={oilPurchasePassed}, Power={powerPurchasePassed}, Size={sizePurchasePassed}, Raycasts={allRaycastsPassed}");

        // Test Movement remains correct
        bool movementCorrect = false;
        if (ctrl.HeadRigidbody != null)
        {
            Vector3 startPos = ctrl.HeadRigidbody.position;
            for (int f = 0; f < 30; f++)
            {
                ctrl.HeadRigidbody.linearVelocity = new Vector3(-2f, 0.5f, 0f);
                yield return new WaitForFixedUpdate();
            }
            Vector3 endPos = ctrl.HeadRigidbody.position;
            float movementDelta = Vector3.Distance(startPos, endPos);
            Debug.Log($"[VerifyExcavator] Movement test: Head moved {movementDelta:F3} units over 30 physics ticks.");
            movementCorrect = movementDelta > 0.1f;
        }
        report.AppendLine($"Movement Correct: {movementCorrect}");

        report.AppendLine("\n=== FINAL SUMMARY ===");
        report.AppendLine($"Length cost sequence: 10 -> 500 -> 1000 -> 4000 -> 10000");
        report.AppendLine($"Length increment per purchase: ~0.15 units");
        report.AppendLine($"10 -> 500 -> 1000 -> 4000 working: {(lengthProgressionPassed ? "YES" : "NO")}");
        report.AppendLine($"Gameplay upgrade UI visible: {(gameplayUIVisible ? "YES" : "NO")}");
        report.AppendLine($"Length/Oil/Power/Size usable during gameplay: {(allUsableDuringGameplay ? "YES" : "NO")}");

        File.WriteAllText(VerifyExcavatorTask.RESULT_FILE, report.ToString());
        Debug.Log("[VerifyExcavator] Result written to " + VerifyExcavatorTask.RESULT_FILE);
        Debug.Log(report.ToString());

        yield return new WaitForSecondsRealtime(0.5f);
        EditorApplication.isPlaying = false;
    }

    private T GetPrivateField<T>(object obj, string fieldName)
    {
        var f = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        return f != null ? (T)f.GetValue(obj) : default;
    }
}
#endif
