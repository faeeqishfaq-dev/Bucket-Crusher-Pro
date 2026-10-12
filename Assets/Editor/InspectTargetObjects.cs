#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
public static class InspectTargetObjects
{
    static InspectTargetObjects()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists("Temp/RunInspectTargets.txt"))
            {
                try { File.Delete("Temp/RunInspectTargets.txt"); } catch {}
                Inspect();
            }
        };
    }

    [MenuItem("Tools/Inspect Target Objects")]
    public static void Inspect()
    {
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);
        StringBuilder sb = new StringBuilder();

        string[] targetNames = new string[]
        {
            "AdsSettings",
            "NoAdsButton",
            "NoAdsCanvas",
            "DrillBoostCanvas",
            "ActivateBoosterCanvas",
            "BoosterSpawner",
            "OilTank",
            "Incremental",
            "Tutorial",
            "ChestRoom",
            "BonusLevelRV"
        };

        var allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        sb.AppendLine("=== TARGET OBJECTS IN GAMEPLAY ===");
        foreach (var t in allTransforms)
        {
            foreach (var target in targetNames)
            {
                if (t.name.IndexOf(target, System.StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    sb.AppendLine($"\nFOUND: {GetPath(t)} (active={t.gameObject.activeSelf})");
                    var comps = t.GetComponents<Component>();
                    foreach (var c in comps)
                    {
                        if (c == null)
                        {
                            sb.AppendLine("  - Component: MISSING_SCRIPT");
                        }
                        else
                        {
                            sb.AppendLine($"  - Component: {c.GetType().FullName}");
                            var serializedObj = new SerializedObject(c);
                            var prop = serializedObj.GetIterator();
                            while (prop.NextVisible(true))
                            {
                                if (prop.propertyType == SerializedPropertyType.ObjectReference && prop.objectReferenceValue != null)
                                {
                                    sb.AppendLine($"      {prop.propertyPath}: {prop.objectReferenceValue.name} ({prop.objectReferenceValue.GetType().Name})");
                                }
                                else if (prop.propertyPath.IndexOf("ad", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         prop.propertyPath.IndexOf("reward", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         prop.propertyPath.IndexOf("rv", System.StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    sb.AppendLine($"      {prop.propertyPath} = {prop.type}");
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        File.WriteAllText("Temp/TargetObjectsDump.txt", sb.ToString());
        Debug.Log("[Inspect] Dump completed to Temp/TargetObjectsDump.txt");
    }

    private static string GetPath(Transform t)
    {
        if (t.parent == null) return t.name;
        return GetPath(t.parent) + "/" + t.name;
    }
}
#endif
