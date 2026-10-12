using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class MissingScriptFixer
{
    static MissingScriptFixer()
    {
        EditorApplication.delayCall += RunFixer;
    }

    private static void RunFixer()
    {
        if (SessionState.GetBool("MissingScriptFixerRun", false))
            return;
            
        SessionState.SetBool("MissingScriptFixerRun", true);
        Debug.Log("--- STARTING MISSING SCRIPT FIXER ---");

        int totalRemoved = 0;
        int filesChanged = 0;
        
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(p => p.EndsWith(".prefab")).ToArray();
        foreach (string path in prefabPaths)
        {
            using (var scope = new PrefabUtility.EditPrefabContentsScope(path))
            {
                GameObject root = scope.prefabContentsRoot;
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(root);
                if (count > 0)
                {
                    // For missing scripts that cannot be resolved, we remove them.
                    int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(root);
                    if (removed > 0)
                    {
                        totalRemoved += removed;
                        filesChanged++;
                        Debug.Log($"Removed {removed} missing scripts from prefab: {path}");
                    }
                }
            }
        }

        string[] scenePaths = AssetDatabase.GetAllAssetPaths().Where(p => p.EndsWith(".unity")).ToArray();
        foreach (string path in scenePaths)
        {
            Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
            bool sceneChanged = false;
            foreach (GameObject go in scene.GetRootGameObjects())
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                if (count > 0)
                {
                    int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    if (removed > 0)
                    {
                        totalRemoved += removed;
                        sceneChanged = true;
                    }
                }
            }
            if (sceneChanged)
            {
                filesChanged++;
                EditorSceneManager.SaveScene(scene);
                Debug.Log($"Removed missing scripts from scene: {path}");
            }
            EditorSceneManager.CloseScene(scene, true);
        }

        Debug.Log($"--- FINISHED: Removed {totalRemoved} missing scripts across {filesChanged} files ---");
        
        // Write the result to a file so Python can read it
        File.WriteAllText("MissingScriptResult.txt", $"{totalRemoved},{filesChanged}");
    }
}
