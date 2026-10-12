#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CheckColliders
{
    [MenuItem("Tools/Find Negative Scale Colliders")]
    public static void FindAll()
    {
        string[] scenes = new string[]
        {
            "Assets/Scenes/Preloader.unity",
            "Assets/Scenes/GamePlay.unity",
            "Assets/Scenes/GamePlay_Transition.unity",
            "Assets/Scenes/DuelGame.unity",
            "Assets/Scenes/Drawing.unity"
        };

        foreach (var s in scenes)
        {
            Scene scene = EditorSceneManager.OpenScene(s, OpenSceneMode.Single);
            BoxCollider[] colliders = Object.FindObjectsByType<BoxCollider>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var col in colliders)
            {
                Vector3 scale = col.transform.lossyScale;
                if (scale.x < 0 || scale.y < 0 || scale.z < 0 || col.size.x < 0 || col.size.y < 0 || col.size.z < 0)
                {
                    string path = GetPath(col.transform);
                    Debug.LogWarning($"[NegativeCollider] Scene '{s}' -> Path: '{path}', lossyScale: {scale}, size: {col.size}");
                }
            }
        }
        EditorSceneManager.OpenScene("Assets/Scenes/Preloader.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Fix Negative Scale Colliders")]
    public static void FixAll()
    {
        string[] scenes = new string[]
        {
            "Assets/Scenes/GamePlay.unity",
            "Assets/Scenes/GamePlay_Transition.unity",
            "Assets/Scenes/DuelGame.unity"
        };

        foreach (var s in scenes)
        {
            Scene scene = EditorSceneManager.OpenScene(s, OpenSceneMode.Single);
            bool dirty = false;

            // 1. Remove duplicate BoxColliders from TreadmillVisualsLeft
            var leftVisuals = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var t in leftVisuals)
            {
                if (t.name == "TreadmillVisualsLeft")
                {
                    var cols = t.GetComponentsInChildren<BoxCollider>(true);
                    foreach (var c in cols)
                    {
                        Debug.Log($"[FixCollider] Scene '{s}' -> Removing duplicate BoxCollider from '{GetPath(c.transform)}'");
                        Object.DestroyImmediate(c, true);
                        dirty = true;
                    }
                }

                // 2. Fix Map/Cube: replace BoxCollider with convex MeshCollider as recommended by Unity PhysX
                if (t.name == "Cube" && t.parent != null && t.parent.name == "Map")
                {
                    var box = t.GetComponent<BoxCollider>();
                    if (box != null)
                    {
                        var mf = t.GetComponent<MeshFilter>();
                        if (mf != null && mf.sharedMesh != null)
                        {
                            Debug.Log($"[FixCollider] Scene '{s}' -> Replacing BoxCollider on 'Map/Cube' with convex MeshCollider");
                            var isTrigger = box.isTrigger;
                            var mat = box.sharedMaterial;
                            Object.DestroyImmediate(box, true);
                            var mc = t.gameObject.AddComponent<MeshCollider>();
                            mc.sharedMesh = mf.sharedMesh;
                            mc.convex = true;
                            mc.isTrigger = isTrigger;
                            mc.sharedMaterial = mat;
                            dirty = true;
                        }
                    }
                }
            }

            if (dirty)
            {
                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
                Debug.Log($"[FixCollider] Saved modified scene: {s}");
            }
        }

        EditorSceneManager.OpenScene("Assets/Scenes/Preloader.unity", OpenSceneMode.Single);
    }

    private static string GetPath(Transform t)
    {
        if (t.parent == null) return t.name;
        return GetPath(t.parent) + "/" + t.name;
    }
}
#endif
