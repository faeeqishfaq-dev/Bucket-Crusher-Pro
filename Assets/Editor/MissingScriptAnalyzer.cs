using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MissingScriptAnalyzer
{
    public static void Analyze()
    {
        int missingCount = 0;
        
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(p => p.EndsWith(".prefab")).ToArray();
        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;
            
            GameObject[] allTrans = prefab.GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToArray();
            foreach (GameObject go in allTrans)
            {
                Component[] components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        missingCount++;
                        Debug.Log($"Missing script found in Prefab: {path} on GameObject: {go.name}");
                    }
                }
            }
        }

        string[] scenePaths = AssetDatabase.GetAllAssetPaths().Where(p => p.EndsWith(".unity")).ToArray();
        foreach (string path in scenePaths)
        {
            // Scenes are harder to iterate without opening, but we can do a quick check
            var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path, UnityEditor.SceneManagement.OpenSceneMode.Additive);
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject go in rootObjects)
            {
                Transform[] allTrans = go.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in allTrans)
                {
                    Component[] components = t.GetComponents<Component>();
                    for (int i = 0; i < components.Length; i++)
                    {
                        if (components[i] == null)
                        {
                            missingCount++;
                            Debug.Log($"Missing script found in Scene: {path} on GameObject: {t.name}");
                        }
                    }
                }
            }
            UnityEditor.SceneManagement.EditorSceneManager.CloseScene(scene, true);
        }

        Debug.Log("TOTAL_MISSING: " + missingCount);
        EditorApplication.Exit(0);
    }
}
