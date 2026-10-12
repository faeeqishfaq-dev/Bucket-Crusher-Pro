#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class MissingScriptCleaner
{
    private const string TRIGGER_FILE = "Temp/RunCleanMissingScripts.txt";

    static MissingScriptCleaner()
    {
        EditorApplication.update += CheckTrigger;
    }

    private static void CheckTrigger()
    {
        if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;

        if (File.Exists(TRIGGER_FILE))
        {
            try { File.Delete(TRIGGER_FILE); } catch {}
            CleanAllMissingScripts();
        }

        if (File.Exists("Temp/RunCountMissingScripts.txt"))
        {
            try { File.Delete("Temp/RunCountMissingScripts.txt"); } catch {}
            CountAllMissingScripts();
        }
    }

    [MenuItem("Tools/Remove All Missing Scripts")]
    public static void CleanAllMissingScripts()
    {
        Debug.Log("[MissingScriptCleaner] === STARTING MISSING SCRIPT REMOVAL SCAN ===");
        int totalPrefabsCleaned = 0;
        int totalPrefabScriptsRemoved = 0;

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        Debug.Log($"[MissingScriptCleaner] Scanning {prefabGuids.Length} prefabs...");

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                GameObject root = PrefabUtility.LoadPrefabContents(path);
                if (root == null) continue;

                int removedInPrefab = CleanHierarchy(root);
                if (removedInPrefab > 0)
                {
                    PrefabUtility.SaveAsPrefabAsset(root, path);
                    totalPrefabsCleaned++;
                    totalPrefabScriptsRemoved += removedInPrefab;
                    Debug.Log($"[MissingScriptCleaner] Cleaned {removedInPrefab} missing script(s) from prefab: {path}");
                }
                PrefabUtility.UnloadPrefabContents(root);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[MissingScriptCleaner] Error processing prefab {path}: {ex.Message}");
            }
        }

        int totalScenesCleaned = 0;
        int totalSceneScriptsRemoved = 0;

        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        Debug.Log($"[MissingScriptCleaner] Scanning {sceneGuids.Length} scenes...");

        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                int removedInScene = 0;
                GameObject[] roots = scene.GetRootGameObjects();
                foreach (GameObject r in roots)
                {
                    removedInScene += CleanHierarchy(r);
                }

                if (removedInScene > 0)
                {
                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorSceneManager.SaveScene(scene);
                    totalScenesCleaned++;
                    totalSceneScriptsRemoved += removedInScene;
                    Debug.Log($"[MissingScriptCleaner] Cleaned {removedInScene} missing script(s) from scene: {path}");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[MissingScriptCleaner] Error processing scene {path}: {ex.Message}");
            }
        }

        // Return to Preloader scene
        EditorSceneManager.OpenScene("Assets/Scenes/Preloader.unity", OpenSceneMode.Single);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"[MissingScriptCleaner] === FINISHED MISSING SCRIPT REMOVAL ===");
        Debug.Log($"[MissingScriptCleaner] Total prefabs cleaned: {totalPrefabsCleaned}, missing scripts removed from prefabs: {totalPrefabScriptsRemoved}");
        Debug.Log($"[MissingScriptCleaner] Total scenes cleaned: {totalScenesCleaned}, missing scripts removed from scenes: {totalSceneScriptsRemoved}");
        Debug.Log($"[MissingScriptCleaner] TOTAL MISSING SCRIPTS REMOVED: {totalPrefabScriptsRemoved + totalSceneScriptsRemoved}");
    }

    private static int CleanHierarchy(GameObject go)
    {
        int count = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            count += CleanHierarchy(go.transform.GetChild(i).gameObject);
        }
        return count;
    }

    [MenuItem("Tools/Count Missing Scripts")]
    public static int CountAllMissingScripts()
    {
        int totalMissing = 0;
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                GameObject root = PrefabUtility.LoadPrefabContents(path);
                if (root != null)
                {
                    int m = CountHierarchy(root);
                    if (m > 0)
                    {
                        Debug.LogWarning($"[MissingScriptCleaner] Prefab '{path}' has {m} missing script(s)");
                        totalMissing += m;
                    }
                    PrefabUtility.UnloadPrefabContents(root);
                }
            }
            catch {}
        }

        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                int m = 0;
                foreach (GameObject r in scene.GetRootGameObjects())
                {
                    m += CountHierarchy(r);
                }
                if (m > 0)
                {
                    Debug.LogWarning($"[MissingScriptCleaner] Scene '{path}' has {m} missing script(s)");
                    totalMissing += m;
                }
            }
            catch {}
        }

        EditorSceneManager.OpenScene("Assets/Scenes/Preloader.unity", OpenSceneMode.Single);
        Debug.Log($"[MissingScriptCleaner] Total missing scripts across project: {totalMissing}");
        return totalMissing;
    }

    private static int CountHierarchy(GameObject go)
    {
        int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            count += CountHierarchy(go.transform.GetChild(i).gameObject);
        }
        return count;
    }
}
#endif
