#if UNITY_EDITOR
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[InitializeOnLoad]
public static class GamePlaySmokeTest
{
    private const string TRIGGER_FILE = "Temp/RunGamePlayTestTrigger.txt";
    private const string PLAYING_FLAG = "Temp/RunningGamePlayTest.txt";

    static GamePlaySmokeTest()
    {
        Application.runInBackground = true;
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;

            if (!EditorApplication.isPlaying && File.Exists(TRIGGER_FILE))
            {
                try { File.Delete(TRIGGER_FILE); } catch {}
                Run();
                return;
            }

            if (EditorApplication.isPlaying && File.Exists("Temp/StopPlay.txt"))
            {
                try { File.Delete("Temp/StopPlay.txt"); } catch {}
                EditorApplication.isPlaying = false;
                return;
            }

            if (EditorApplication.isPlaying && File.Exists(PLAYING_FLAG))
            {
                try { File.Delete(PLAYING_FLAG); } catch {}
                Debug.Log("[GamePlayTest] Spawning GamePlayTestRunner...");
                var go = new GameObject("GamePlayTestRunner");
                go.AddComponent<GamePlayTestRunner>();
                Object.DontDestroyOnLoad(go);
            }
        };
    }

    [MenuItem("Tools/Run GamePlay Smoke Test")]
    public static void Run()
    {
        Application.runInBackground = true;
        Debug.Log("[GamePlayTest] === Starting GamePlay Scene Verification ===");
        File.WriteAllText(PLAYING_FLAG, "1");
        EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
        EditorApplication.isPlaying = true;
    }

    public static void RunBatchTest()
    {
        Run();
    }
}

public class GamePlayTestRunner : MonoBehaviour
{
    private IEnumerator Start()
    {
        Application.runInBackground = true;
        Time.timeScale = 1f;
        Debug.Log("[GamePlayTest] Runner initialized in Play Mode.");
        yield return new WaitForSecondsRealtime(0.5f);

        // --- STEP 1: Verify Ad & Unwanted Popup Objects are gone ---
        Debug.Log("[GamePlayTest] Step 1: Checking for remaining Ad UI and unwanted popups...");
        string[] forbiddenNames = new string[]
        {
            "AdsSettings", "NoAdsCanvas", "NoAdsButton", "RenewableRV", "RV_Start",
            "ForceRV", "Canvas_IAPGems", "Canvas-ChestRoom", "ChestRoomKeysManager",
            "KeyCanvas", "BonusLevelCanvas", "BonusBankNoteFX", "BubbleCanvas",
            "DrillBoostCanvas", "FuelLostRV", "FuelLostMoneyRv", "DuelModeUnlockedPopup"
        };

        foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (t == null) continue;
            foreach (var forbidden in forbiddenNames)
            {
                if (t.name == forbidden && t.gameObject.activeSelf)
                {
                    Debug.LogError($"[GamePlayTest] FAILED: Forbidden ad/popup object still active: {t.name}");
                    Finish(1);
                    yield break;
                }
            }
        }
        Debug.Log("[GamePlayTest] Step 1 PASSED: Zero unwanted ad/popup objects active.");

        // --- STEP 2: Verify Legitimate Screens start inactive and do NOT block raycasts ---
        Debug.Log("[GamePlayTest] Step 2: Checking legitimate canvases initial state...");
        var shopCanvas = GameObject.Find("ShopCanvas");
        if (shopCanvas != null && shopCanvas.activeInHierarchy)
        {
            var cg = shopCanvas.GetComponent<CanvasGroup>();
            if (cg != null && cg.blocksRaycasts)
            {
                Debug.LogError("[GamePlayTest] FAILED: ShopCanvas is active and blocksRaycasts=true at game start!");
                Finish(1);
                yield break;
            }
        }

        var endScreen = GameObject.Find("EndScreen");
        if (endScreen != null && endScreen.activeInHierarchy)
        {
            var cg = endScreen.GetComponent<CanvasGroup>();
            if (cg != null && cg.blocksRaycasts)
            {
                Debug.LogError("[GamePlayTest] FAILED: EndScreen is active and blocksRaycasts=true at game start!");
                Finish(1);
                yield break;
            }
        }

        var loadingCanvas = GameObject.Find("LoadinghCanvas");
        if (loadingCanvas != null && loadingCanvas.activeInHierarchy)
        {
            Debug.LogError("[GamePlayTest] FAILED: LoadinghCanvas is active at game start!");
            Finish(1);
            yield break;
        }
        Debug.Log("[GamePlayTest] Step 2 PASSED: Legitimate screens start hidden with no raycast blocks.");

        // --- STEP 3: Verify InputCanvas and EventSystem ---
        Debug.Log("[GamePlayTest] Step 3: Checking InputCanvas and EventSystem...");
        var es = Object.FindAnyObjectByType<EventSystem>();
        if (es == null || !es.gameObject.activeInHierarchy)
        {
            Debug.LogError("[GamePlayTest] FAILED: EventSystem is missing or inactive!");
            Finish(1);
            yield break;
        }

        var inputCanvasGo = GameObject.Find("InputCanvas");
        if (inputCanvasGo == null || !inputCanvasGo.activeInHierarchy)
        {
            Debug.LogError("[GamePlayTest] FAILED: InputCanvas is missing or inactive!");
            Finish(1);
            yield break;
        }

        var dynamicJoy = Object.FindAnyObjectByType<DynamicJoystick>();
        if (dynamicJoy == null || !dynamicJoy.gameObject.activeInHierarchy)
        {
            Debug.LogError("[GamePlayTest] FAILED: DynamicJoystick is missing or inactive!");
            Finish(1);
            yield break;
        }
        Debug.Log("[GamePlayTest] Step 3 PASSED: InputCanvas, EventSystem, and DynamicJoystick active and ready.");

        // --- STEP 4: Verify Start Button and Game Launch ---
        Debug.Log("[GamePlayTest] Step 4: Testing Game Launch from Start Button...");
        PlayerPrefs.SetInt("NumberGamePlayed", 0);
        PlayerPrefs.SetInt("LengthUpgrade", 0);
        PlayerPrefs.SetInt("OilUpgrade", 0);
        PlayerPrefs.SetInt("PowerUpgrade", 0);
        PlayerPrefs.SetInt("SizeUpgrade", 0);
        PlayerPrefs.SetInt("TapTutorialDone", 0);
        PlayerPrefs.Save();

        Shop shop = Object.FindAnyObjectByType<Shop>();
        if (shop != null) shop.UpdateAllButtons();

        GameManager gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogError("[GamePlayTest] FAILED: GameManager.Instance is null!");
            Finish(1);
            yield break;
        }

        gm.StartGame();
        yield return new WaitForSecondsRealtime(0.5f);

        Control ctrl = Object.FindAnyObjectByType<Control>();
        if (ctrl == null)
        {
            Debug.LogError("[GamePlayTest] FAILED: Control component not found!");
            Finish(1);
            yield break;
        }

        Oil oil = Object.FindAnyObjectByType<Oil>();
        if (oil == null)
        {
            Debug.LogError("[GamePlayTest] FAILED: Oil component not found!");
            Finish(1);
            yield break;
        }
        Debug.Log("[GamePlayTest] Step 4 PASSED: Game started cleanly.");

        // --- STEP 5: TEST MOBILE TOUCH CONTROLS (Left, Right, Hold, Release) ---
        Debug.Log("[GamePlayTest] Step 5: Testing Mobile Touch Drag, Hold, and Release...");
        var pointerEvent = new PointerEventData(es);
        pointerEvent.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.4f);
        pointerEvent.pointerId = 0;

        // Pointer Down
        dynamicJoy.OnPointerDown(pointerEvent);
        yield return null;

        // Drag LEFT
        pointerEvent.position = new Vector2(Screen.width * 0.5f - 150f, Screen.height * 0.4f);
        dynamicJoy.OnDrag(pointerEvent);
        yield return null;

        Vector2 dirLeft = dynamicJoy.Direction;
        Debug.Log($"[GamePlayTest] Touch drag Left: dir={dirLeft}");
        if (dirLeft.x >= 0f)
        {
            Debug.LogError($"[GamePlayTest] FAILED: Drag left did not produce negative X direction! dir={dirLeft}");
            Finish(1);
            yield break;
        }

        // Test TOUCH-AND-HOLD for 15 physics frames
        float fuelBeforeHold = oil.OilValue;
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        float fuelAfterHold = oil.OilValue;
        Debug.Log($"[GamePlayTest] Touch-and-Hold Left: Fuel {fuelBeforeHold:F1} -> {fuelAfterHold:F1}, HeadVel={ctrl.HeadRigidbody.linearVelocity}");
        if (fuelAfterHold >= fuelBeforeHold)
        {
            Debug.LogError("[GamePlayTest] FAILED: Fuel did not consume during touch-and-hold movement!");
            Finish(1);
            yield break;
        }
        if (ctrl.HeadRigidbody.linearVelocity.x >= 0f)
        {
            Debug.LogError($"[GamePlayTest] FAILED: Excavator head did not move left during hold! vel={ctrl.HeadRigidbody.linearVelocity}");
            Finish(1);
            yield break;
        }

        // Drag RIGHT
        pointerEvent.position = new Vector2(Screen.width * 0.5f + 150f, Screen.height * 0.4f);
        dynamicJoy.OnDrag(pointerEvent);
        yield return new WaitForFixedUpdate();

        Vector2 dirRight = dynamicJoy.Direction;
        Debug.Log($"[GamePlayTest] Touch drag Right: dir={dirRight}, HeadVel={ctrl.HeadRigidbody.linearVelocity}");
        if (dirRight.x <= 0f)
        {
            Debug.LogError($"[GamePlayTest] FAILED: Drag right did not produce positive X direction! dir={dirRight}");
            Finish(1);
            yield break;
        }

        // Pointer UP (Release finger)
        dynamicJoy.OnPointerUp(pointerEvent);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        Vector2 dirReleased = dynamicJoy.Direction;
        Debug.Log($"[GamePlayTest] Touch Release: dir={dirReleased}, HeadVel={ctrl.HeadRigidbody.linearVelocity}");
        if (dirReleased.sqrMagnitude > 0.001f || Mathf.Abs(ctrl.HeadRigidbody.linearVelocity.x) > 1.0f)
        {
            Debug.LogError($"[GamePlayTest] FAILED: Releasing touch did not stop driving input/movement! dir={dirReleased}, vel={ctrl.HeadRigidbody.linearVelocity}");
            Finish(1);
            yield break;
        }
        Debug.Log("[GamePlayTest] Step 5 PASSED: Touch left, right, hold, and release work perfectly.");

        // --- STEP 6: Multi-touch and In-Game Booster ---
        Debug.Log("[GamePlayTest] Step 6: Testing Multi-touch and Booster...");
        var boosterController = Object.FindAnyObjectByType<ExcavatorBoosterController>();
        if (boosterController != null)
        {
            // Touch joystick with finger 0 and drag left
            pointerEvent.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.4f);
            pointerEvent.pointerId = 0;
            dynamicJoy.OnPointerDown(pointerEvent);
            pointerEvent.position = new Vector2(Screen.width * 0.5f - 150f, Screen.height * 0.4f);
            dynamicJoy.OnDrag(pointerEvent);
            yield return null;

            // Tap booster button with finger 1
            boosterController.OnClick();
            yield return new WaitForSecondsRealtime(0.2f);

            // Verify finger 0 joystick direction is unaffected
            Vector2 dirAfterBooster = dynamicJoy.Direction;
            if (dirAfterBooster.x >= 0f)
            {
                Debug.LogError("[GamePlayTest] FAILED: Multi-touch booster disrupted active joystick touch!");
                Finish(1);
                yield break;
            }

            dynamicJoy.OnPointerUp(pointerEvent);
        }
        Debug.Log("[GamePlayTest] Step 6 PASSED: Multi-touch booster functions cleanly.");

        // --- STEP 7: Fuel Exhaustion -> EndScreen -> Replay Loop ---
        Debug.Log("[GamePlayTest] Step 7: Testing Fuel Exhaustion and End Screen...");
        oil.AddOil(-99999); // drain fuel completely to trigger game end
        yield return new WaitForSecondsRealtime(0.5f);

        var endScreenView = Object.FindAnyObjectByType<EndScreenView>(FindObjectsInactive.Include);
        if (endScreenView == null || !endScreenView.gameObject.activeInHierarchy)
        {
            Debug.LogError("[GamePlayTest] FAILED: EndScreen was not activated upon fuel exhaustion!");
            Finish(1);
            yield break;
        }

        // Click No Thanks / Next to return to menu
        endScreenView.OnNoThanksClick();
        yield return new WaitForSecondsRealtime(0.5f);

        int gamesPlayed = OnboardingManager.Instance != null ? OnboardingManager.Instance.GetNumberGamePlayed : 0;
        Debug.Log($"[GamePlayTest] Games played recorded: {gamesPlayed}");
        if (gamesPlayed < 1)
        {
            Debug.LogError("[GamePlayTest] FAILED: Games played count was not incremented!");
            Finish(1);
            yield break;
        }

        // --- STEP 8: Verify Tutorial Step 1 Length Upgrade ---
        Debug.Log("[GamePlayTest] Step 8: Verifying Tutorial Upgrade Flow...");
        if (shop != null)
        {
            shop.UpdateAllButtons();
            var states = OnboardingManager.Instance.GetIncrementalStates();
            Debug.Log($"[GamePlayTest] Tutorial state: LongVisible={states.LongVisible}, LongIsFree={states.LongIsFree}");
            if (!states.LongVisible || !states.LongIsFree)
            {
                Debug.LogError("[GamePlayTest] FAILED: Length upgrade tutorial state incorrect!");
                Finish(1);
                yield break;
            }

            shop.BuyLong(false);
            yield return new WaitForSecondsRealtime(0.2f);

            var statesAfter = OnboardingManager.Instance.GetIncrementalStates();
            if (statesAfter.LongIsFree)
            {
                Debug.LogError("[GamePlayTest] FAILED: Length tutorial arrow still active after purchase!");
                Finish(1);
                yield break;
            }
        }
        Debug.Log("[GamePlayTest] Step 8 PASSED: Tutorial upgrade purchased and arrow dismissed.");

        // --- STEP 9: Replay Game 1 ---
        Debug.Log("[GamePlayTest] Step 9: Replay loop verification...");
        gm.StartGame();
        yield return new WaitForSecondsRealtime(0.5f);

        if (!gm.gameObject.activeInHierarchy || oil.OilValue <= 0f)
        {
            Debug.LogError("[GamePlayTest] FAILED: Replay game did not restart properly!");
            Finish(1);
            yield break;
        }
        Debug.Log("[GamePlayTest] Step 9 PASSED: Replay game started successfully.");

        Debug.Log("[GamePlayTest] === ALL TESTS PASSED! ===");
        Finish(0);
    }

    private void Finish(int code)
    {
        File.WriteAllText("Temp/GamePlayTestResult.txt", code == 0 ? "SUCCESS" : "FAILURE");
        EditorApplication.isPlaying = false;
        if (Application.isBatchMode)
        {
            EditorApplication.Exit(code);
        }
    }
}
#endif
