#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
public static class CleanGamePlayAdsAndFixTutorial
{
    static CleanGamePlayAdsAndFixTutorial()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists("Temp/RunCleanAds.txt"))
            {
                try { File.Delete("Temp/RunCleanAds.txt"); } catch {}
                Execute();
            }
        };
    }

    [MenuItem("Tools/Clean GamePlay Ads & Fix Tutorial")]
    public static void Execute()
    {
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);
        bool dirty = false;

        // 1. Delete root AdsSettings if present & activate root UI
        var rootGos = scene.GetRootGameObjects();
        foreach (var go in rootGos)
        {
            if (go.name == "AdsSettings")
            {
                Debug.Log("[CleanAds] Destroying root AdsSettings GameObject");
                Object.DestroyImmediate(go);
                dirty = true;
            }
            if (go.name == "UI")
            {
                Debug.Log("[CleanAds] Activating root UI GameObject");
                go.SetActive(true);
                dirty = true;
            }
        }

        // 2. Delete NoAdsCanvas if present
        foreach (var t in Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (t != null && t.name == "NoAdsCanvas")
            {
                Debug.Log($"[CleanAds] Destroying NoAdsCanvas at '{t.name}'");
                Object.DestroyImmediate(t.gameObject);
                dirty = true;
                break;
            }
        }

        // 3. Remove all NoAdsButton instances
        var allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "NoAdsButton")
            {
                Debug.Log($"[CleanAds] Destroying NoAdsButton at '{GetPath(t)}'");
                Object.DestroyImmediate(t.gameObject);
                dirty = true;
            }
        }

        // 4. Remove RenewableRV (rewarded ad start button)
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "RenewableRV")
            {
                Debug.Log($"[CleanAds] Destroying RenewableRV at '{GetPath(t)}'");
                Object.DestroyImmediate(t.gameObject);
                dirty = true;
            }
        }

        // 5. Remove RV_Start inside Start button
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "RV_Start")
            {
                Debug.Log($"[CleanAds] Destroying RV_Start at '{GetPath(t)}'");
                Object.DestroyImmediate(t.gameObject);
                dirty = true;
            }
        }

        // 6. Remove RVButton and ForceRV under Incremental
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t == null) continue;
            if (t.name.StartsWith("RVButton") || t.name == "ForceRV")
            {
                Debug.Log($"[CleanAds] Destroying ad upgrade button '{t.name}' at '{GetPath(t)}'");
                Object.DestroyImmediate(t.gameObject);
                dirty = true;
            }
        }

        // 7. Ensure Start button is active and visible
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "Start" && t.parent != null && t.parent.name == "StartButtonGroup")
            {
                Debug.Log($"[CleanAds] Activating Start GameObject at '{GetPath(t)}'");
                t.gameObject.SetActive(true);
                dirty = true;
            }
        }

        // 8. On ActivateBoosterCanvas, remove the video ad badge (Image (4))
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "ActivateBoosterCanvas")
            {
                var img4 = t.Find("Button/Image (4)");
                if (img4 != null)
                {
                    Debug.Log($"[CleanAds] Destroying video badge on ActivateBoosterCanvas at '{GetPath(img4)}'");
                    Object.DestroyImmediate(img4.gameObject);
                    dirty = true;
                }
            }
        }

        // 9. On EndScreen, deactivate RVArea (watch ad panel)
        allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in allTransforms)
        {
            if (t != null && t.name == "RVArea" && t.parent != null && t.parent.name == "EndScreen")
            {
                Debug.Log($"[CleanAds] Deactivating EndScreen RVArea at '{GetPath(t)}'");
                t.gameObject.SetActive(false);
                dirty = true;
            }
        }

        if (dirty)
        {
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log("[CleanAds] Successfully cleaned GamePlay scene and saved.");
        }

        File.WriteAllText("Temp/CleanAdsComplete.txt", "DONE");
    }

    private static string GetPath(Transform t)
    {
        if (t.parent == null) return t.name;
        return GetPath(t.parent) + "/" + t.name;
    }
}
#endif
