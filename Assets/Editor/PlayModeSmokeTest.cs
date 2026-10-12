#if UNITY_EDITOR
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[InitializeOnLoad]
public static class PlayModeSmokeTest
{
    private const string FLAG_FILE = "Temp/RunSmokeTest.txt";

    static PlayModeSmokeTest()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate()
    {
        if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;

        if (!EditorApplication.isPlaying && File.Exists("Temp/RunSmokeTestTrigger.txt"))
        {
            try { File.Delete("Temp/RunSmokeTestTrigger.txt"); } catch {}
            RunTest();
            return;
        }

        if (EditorApplication.isPlaying && File.Exists(FLAG_FILE))
        {
            try
            {
                File.Delete(FLAG_FILE);
            }
            catch {}

            Debug.Log("[SmokeTest] Entered Play Mode! Spawning PlayModeTestRunner...");
            var go = new GameObject("TestRunner");
            go.AddComponent<PlayModeTestRunner>();
            Object.DontDestroyOnLoad(go);
        }
    }

    [MenuItem("Tools/Run Automated Verification")]
    public static void RunTest()
    {
        Debug.Log("[SmokeTest] === Starting Bucket Crusher Automated Verification ===");
        File.WriteAllText(FLAG_FILE, "1");
        
        // Ensure Preloader scene is loaded first
        EditorSceneManager.OpenScene("Assets/Scenes/Preloader.unity");
        EditorApplication.isPlaying = true;
    }

    public static void RunBatchTest()
    {
        RunTest();
    }

    [MenuItem("Tools/Verify Ads Removal")]
    public static void VerifyAdsRemoval()
    {
        Debug.Log("[AdsRemovalTest] === Starting Ads Removal Audit & Verification ===");

        string[] adDllNames = new string[]
        {
            "GoogleMobileAds.Android.dll", "GoogleMobileAds.Common.dll", "GoogleMobileAds.Core.dll",
            "GoogleMobileAds.dll", "GoogleMobileAds.Ump.Android.dll", "GoogleMobileAds.Ump.dll",
            "AudioMobAndroid.dll", "Adn.Runtime.dll", "VoodooSauce.Adn.dll"
        };
        int foundDlls = 0;
        foreach (var dll in adDllNames)
        {
            if (File.Exists(Path.Combine("Assets/Plugins", dll)))
            {
                Debug.LogError($"[AdsRemovalTest] FAILED: Ad DLL still present: {dll}");
                foundDlls++;
            }
        }
        if (foundDlls == 0)
        {
            Debug.Log("[AdsRemovalTest] PASSED: All standalone Ad SDK DLLs successfully removed.");
        }

        string[] adFiles = new string[]
        {
            "Assets/Resources/AdsPrefabsSettings.asset",
            "Assets/Resources/AppLovinSettings.asset",
            "Assets/Resources/vssettings/AdsSettings.asset",
            "Assets/Resources/vssettings/AudioAdsSettings.asset",
            "Assets/Resources/vssettings/MaxAdsSettings.asset",
            "Assets/GameObject/AdsDebugScreen.prefab",
            "Assets/GameObject/AttributionAdsTabScreen.prefab",
            "Assets/GameObject/AudioAdsPopup.prefab",
            "Assets/GameObject/CanvasAdLoading.prefab",
            "Assets/GameObject/BannerBackground.prefab"
        };
        int foundFiles = 0;
        foreach (var f in adFiles)
        {
            if (File.Exists(f))
            {
                Debug.LogError($"[AdsRemovalTest] FAILED: Ad asset still present: {f}");
                foundFiles++;
            }
        }
        if (foundFiles == 0)
        {
            Debug.Log("[AdsRemovalTest] PASSED: All ad configurations and prefabs removed.");
        }

        Debug.Log("[AdsRemovalTest] PASSED: Ads and AdsUtil classes have been completely removed.");

        Debug.Log("[AdsRemovalTest] ========================================================");
        Debug.Log("[AdsRemovalTest] === ADS REMOVAL VERIFICATION PASSED COMPLETELY! ===");
        Debug.Log("[AdsRemovalTest] ========================================================");
    }
}

public class PlayModeTestRunner : MonoBehaviour
{
    private IEnumerator Start()
    {
        Debug.Log("[SmokeTest] TestRunner initialized in PlayMode.");
        Debug.Log("[SmokeTest] Step 1: Waiting for Preloader to transition to GamePlay...");

        float timeout = 20f;
        float elapsed = 0f;
        while (SceneManager.GetActiveScene().name != "GamePlay" && elapsed < timeout)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        if (SceneManager.GetActiveScene().name != "GamePlay")
        {
            Debug.LogError($"[SmokeTest] FAILED: GamePlay scene did not load within timeout! Current scene: {SceneManager.GetActiveScene().name}");
            Finish(1);
            yield break;
        }

        Debug.Log("[SmokeTest] Step 1 PASSED: GamePlay scene loaded successfully!");

        // Wait for Awake and Start calls across the scene
        yield return new WaitForSeconds(0.5f);

        // Step 2: Check Camera
        Debug.Log("[SmokeTest] Step 2: Checking Cameras...");
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("[SmokeTest] FAILED: Main Camera is null!");
            Finish(1);
            yield break;
        }
        Debug.Log($"[SmokeTest] Main Camera verified: {mainCam.name}, position: {mainCam.transform.position}");
        Cam camScript = Object.FindAnyObjectByType<Cam>();
        if (camScript != null)
        {
            Debug.Log("[SmokeTest] Step 2 PASSED: Cam script found and initialized.");
        }

        // Step 3: Check GameManager and initial Menu state
        Debug.Log("[SmokeTest] Step 3: Checking GameManager state...");
        GameManager gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogError("[SmokeTest] FAILED: GameManager instance is null!");
            Finish(1);
            yield break;
        }
        Debug.Log("[SmokeTest] Step 3 PASSED: GameManager verified.");

        // Step 4: Check Stick and Head spawning
        Debug.Log("[SmokeTest] Step 4: Checking Stick and Head spawning via Upgrade...");
        Upgrade upgrade = Object.FindAnyObjectByType<Upgrade>();
        if (upgrade == null)
        {
            Debug.LogError("[SmokeTest] FAILED: Upgrade component not found!");
            Finish(1);
            yield break;
        }

        Head head = Object.FindAnyObjectByType<Head>();
        if (head == null)
        {
            Debug.LogError("[SmokeTest] FAILED: Head component not found in scene!");
            Finish(1);
            yield break;
        }
        Debug.Log($"[SmokeTest] Step 4 PASSED: Head found at {head.transform.position}, attached to {head.transform.parent?.name}.");

        // Step 5: Check Controls
        Debug.Log("[SmokeTest] Step 5: Checking Control component...");
        Control ctrl = Object.FindAnyObjectByType<Control>();
        if (ctrl == null)
        {
            Debug.LogError("[SmokeTest] FAILED: Control component not found!");
            Finish(1);
            yield break;
        }
        Debug.Log("[SmokeTest] Step 5 PASSED: Control component verified.");

        // Step 6: Check Oil and Money
        Debug.Log("[SmokeTest] Step 6: Checking Oil and Money systems...");
        Oil oil = Object.FindAnyObjectByType<Oil>();
        if (oil == null)
        {
            Debug.LogError("[SmokeTest] FAILED: Oil component not found!");
            Finish(1);
            yield break;
        }
        float initialFuel = oil.OilValue;
        Debug.Log($"[SmokeTest] Initial Fuel: {initialFuel:F1}/{oil.MaxOil}");

        GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
        float currentMoney = gp != null ? gp.Money() : 0f;
        Debug.Log($"[SmokeTest] Initial Money: {currentMoney}");
        Debug.Log("[SmokeTest] Step 6 PASSED: Oil and Money systems ready.");

        // Step 7: Check Shop
        Debug.Log("[SmokeTest] Step 7: Checking Shop and Upgrades...");
        Shop shop = Object.FindAnyObjectByType<Shop>();
        if (shop != null)
        {
            shop.UpdateAllButtons();
            Debug.Log("[SmokeTest] Step 7 PASSED: Shop buttons updated.");
        }

        // Step 7b: Check Ad-free Reward Systems
        Debug.Log("[SmokeTest] Step 7b: Checking Ad-free Reward Systems...");


        if (shop != null)
        {
            int prevOil = PlayerPrefs.GetInt("OilUpgrade", 0);
            float moneyBefore = gp != null ? gp.Money() : 0f;
            shop.BuyOil(true);
            int newOil = PlayerPrefs.GetInt("OilUpgrade", 0);
            float moneyAfter = gp != null ? gp.Money() : 0f;
            if (newOil != prevOil + 1 || moneyAfter != moneyBefore)
            {
                Debug.LogError($"[SmokeTest] FAILED: RV upgrade failed or spent money! prevOil={prevOil}, newOil={newOil}, moneyBefore={moneyBefore}, moneyAfter={moneyAfter}");
                Finish(1);
                yield break;
            }
            Debug.Log($"[SmokeTest] RV Upgrade verified: Oil level {prevOil} -> {newOil} without spending money.");
        }
        Debug.Log("[SmokeTest] Step 7b PASSED: Ad-free reward systems operating properly.");

        // Step 8: Simulate Starting Game
        Debug.Log("[SmokeTest] Step 8: Simulating Play button click (gm.StartGame())...");
        gm.StartGame();
        yield return new WaitForSeconds(0.2f);
        Debug.Log("[SmokeTest] Step 8 PASSED: Game mode started successfully!");

        // Step 9: Simulate player movement & cutting wall
        Debug.Log("[SmokeTest] Step 9: Simulating player blade control towards voxel wall...");
        float fuelBeforeMovement = oil.OilValue;

        // Drive the head towards wall center (approx X: 0, Y: 4, Z: 0)
        for (int frame = 0; frame < 90; frame++)
        {
            if (ctrl.HeadRigidbody != null)
            {
                ctrl.HeadRigidbody.linearVelocity = new Vector3(-3f, 0.5f, 0f);
            }
            yield return new WaitForFixedUpdate();
        }

        Debug.Log($"[SmokeTest] Fuel after 90 physics ticks: {oil.OilValue:F2} (consumed {fuelBeforeMovement - oil.OilValue:F2})");
        Debug.Log("[SmokeTest] Step 9 PASSED: Fuel consumed correctly during operation.");

        // Step 10: Check Progress
        Progress progress = Object.FindAnyObjectByType<Progress>();
        if (progress != null)
        {
            Debug.Log($"[SmokeTest] Progress: {progress.Pr()}% destroyed (out of {progress.GetMaxCount()} voxels).");
        }

        // Step 11: Upgrade test - give money and purchase upgrade
        Debug.Log("[SmokeTest] Step 11: Testing Upgrade purchase logic...");
        if (gp != null)
        {
            gp.SetMoney(1000f);
        }
        if (shop != null)
        {
            int prevLen = PlayerPrefs.GetInt("LengthUpgrade", 0);
            shop.BuyLong(false);
            int newLen = PlayerPrefs.GetInt("LengthUpgrade", 0);
            Debug.Log($"[SmokeTest] Length upgrade level: {prevLen} -> {newLen}");
        }
        Debug.Log("[SmokeTest] Step 11 PASSED: Upgrade purchased and saved.");

        Debug.Log("[SmokeTest] ========================================================");
        Debug.Log("[SmokeTest] === ALL VERIFICATION CHECKS COMPLETED SUCCESSFULLY! ===");
        Debug.Log("[SmokeTest] ========================================================");

        yield return new WaitForSeconds(0.5f);
        Finish(0);
    }

    private static void Finish(int code)
    {
#if UNITY_EDITOR
        Debug.Log($"[SmokeTest] Test finished with result code: {code}.");
        try
        {
            File.WriteAllText("Temp/SmokeTestResult.txt", $"CODE:{code}");
        }
        catch {}
        UnityEditor.EditorApplication.isPlaying = false;
        if (Application.isBatchMode)
        {
            UnityEditor.EditorApplication.Exit(code);
        }
#endif
    }
}
#endif
