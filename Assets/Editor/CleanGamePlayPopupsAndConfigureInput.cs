#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
public static class CleanGamePlayPopupsAndConfigureInput
{
    private const string TRIGGER_FILE = "Temp/RunCleanPopupsAndInput.txt";

    static CleanGamePlayPopupsAndConfigureInput()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists(TRIGGER_FILE))
            {
                try { File.Delete(TRIGGER_FILE); } catch {}
                Execute();
            }
        };
    }

    [MenuItem("Tools/Clean Popups and Configure Input")]
    public static void Execute()
    {
        Debug.Log("[CleanPopups] === Cleaning Unwanted Popups and Configuring Mobile Input ===");
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);

        // 1. Destroy Pure Ad / Unwanted Popup Objects
        string[] toDestroy = new string[]
        {
            "Canvas_IAPGems",
            "Canvas-ChestRoom",
            "ChestRoomKeysManager",
            "KeyCanvas",
            "BonusLevelCanvas",
            "BonusBankNoteFX",
            "BubbleCanvas",
            "DrillBoostCanvas",
            "FuelLostRV",
            "FuelLostMoneyRv",
            "DuelModeUnlockedPopup",
            "NoAdsCanvas",
            "BetterNoAds_new",
            "NoAdsPopup"
        };

        foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (t == null) continue;
            foreach (var name in toDestroy)
            {
                if (t.name == name)
                {
                    Debug.Log($"[CleanPopups] Destroying: {t.name}");
                    Object.DestroyImmediate(t.gameObject);
                    break;
                }
            }
        }

        // 2. Hide / Reset Legitimate Canvases so they start clean
        var shopCanvas = GameObject.Find("ShopCanvas");
        if (shopCanvas != null)
        {
            var cg = shopCanvas.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
            shopCanvas.SetActive(false);
            Debug.Log("[CleanPopups] ShopCanvas set inactive and blocksRaycasts=false");
        }

        var endScreen = GameObject.Find("EndScreen");
        if (endScreen != null)
        {
            var cg = endScreen.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
            endScreen.SetActive(false);
            Debug.Log("[CleanPopups] EndScreen set inactive and blocksRaycasts=false");
        }

        var loadingCanvas = GameObject.Find("LoadinghCanvas");
        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
            Debug.Log("[CleanPopups] LoadinghCanvas set inactive");
        }

        var settingsCanvas = GameObject.Find("SettingsCanvas");
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
            Debug.Log("[CleanPopups] SettingsCanvas set inactive");
        }

        // 3. Configure InputCanvas for Mobile Touch Input
        var inputCanvasGo = GameObject.Find("InputCanvas");
        if (inputCanvasGo != null)
        {
            inputCanvasGo.SetActive(true);

            var canvas = inputCanvasGo.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 5;
                Debug.Log("[CleanPopups] InputCanvas configured: ScreenSpaceOverlay, sortOrder=5");
            }

            var raycaster = inputCanvasGo.GetComponent<GraphicRaycaster>();
            if (raycaster != null)
            {
                raycaster.enabled = true;
                raycaster.blockingMask = -1;
                raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
            }

            // In JoystickSwitch
            var joySwitch = inputCanvasGo.GetComponentInChildren<InputSwitch>(true);
            if (joySwitch != null)
            {
                var dynamicJoy = joySwitch.transform.Find("Dynamic Joystick");
                var fixedJoy = joySwitch.transform.Find("Fixed Joystick");
                var tapControl = joySwitch.transform.Find("TapControl");

                if (tapControl != null)
                {
                    tapControl.gameObject.SetActive(false);
                    var img = tapControl.GetComponent<Image>();
                    if (img != null) img.raycastTarget = false;
                    Debug.Log("[CleanPopups] TapControl deactivated");
                }

                if (fixedJoy != null)
                {
                    fixedJoy.gameObject.SetActive(false);
                    Debug.Log("[CleanPopups] Fixed Joystick deactivated in favor of Dynamic Joystick");
                }

                if (dynamicJoy != null)
                {
                    dynamicJoy.gameObject.SetActive(true);
                    var djRt = dynamicJoy.GetComponent<RectTransform>();
                    if (djRt != null)
                    {
                        djRt.anchorMin = Vector2.zero;
                        djRt.anchorMax = Vector2.one;
                        djRt.offsetMin = Vector2.zero;
                        djRt.offsetMax = Vector2.zero;
                        djRt.pivot = new Vector2(0.5f, 0.5f);
                    }

                    var bg = dynamicJoy.Find("Background");
                    if (bg != null)
                    {
                        bg.gameObject.SetActive(false);
                        var bgRt = bg.GetComponent<RectTransform>();
                        if (bgRt != null)
                        {
                            bgRt.anchorMin = new Vector2(0.5f, 0.5f);
                            bgRt.anchorMax = new Vector2(0.5f, 0.5f);
                            bgRt.pivot = new Vector2(0.5f, 0.5f);
                        }

                        var handle = bg.Find("Handle");
                        if (handle != null)
                        {
                            var hRt = handle.GetComponent<RectTransform>();
                            if (hRt != null)
                            {
                                hRt.anchorMin = new Vector2(0.5f, 0.5f);
                                hRt.anchorMax = new Vector2(0.5f, 0.5f);
                                hRt.pivot = new Vector2(0.5f, 0.5f);
                                hRt.anchoredPosition = Vector2.zero;
                            }
                        }
                    }

                    Debug.Log("[CleanPopups] Dynamic Joystick configured with full stretch rect");
                }
            }
        }

        // 4. Check EventSystem
        var es = Object.FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>(FindObjectsInactive.Include);
        if (es != null)
        {
            es.gameObject.SetActive(true);
            var module = es.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            if (module != null) module.enabled = true;
            Debug.Log("[CleanPopups] EventSystem verified and active");
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
        Debug.Log("[CleanPopups] === All cleanups applied and GamePlay scene saved successfully! ===");
        File.WriteAllText("Temp/CleanDone.txt", "OK");
    }
}
#endif
