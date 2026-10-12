#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class CheckSwitcherAndManagers
{
    static CheckSwitcherAndManagers()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists("Temp/RunCheckSwitcher.txt"))
            {
                try { File.Delete("Temp/RunCheckSwitcher.txt"); } catch {}
                Run();
            }
        };
    }
    [MenuItem("Tools/Check Switcher and Managers")]
    public static void Run()
    {
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);
        StringBuilder sb = new StringBuilder();

        var switchers = Object.FindObjectsByType<DuelModeStartButtonSwitcher>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        sb.AppendLine($"--- DuelModeStartButtonSwitcher ({switchers.Length}) ---");
        foreach (var s in switchers)
        {
            sb.AppendLine($"Path: {GetPath(s.transform)}, NormalButton={s.NormalButton}, DuelButton={s.DuelButton}");
        }

        var onboarding = Object.FindObjectsByType<OnboardingManager>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        sb.AppendLine($"\n--- OnboardingManager ({onboarding.Length}) ---");
        foreach (var o in onboarding)
        {
            var so = new SerializedObject(o);
            var sp = so.GetIterator();
            while (sp.NextVisible(true))
            {
                sb.AppendLine($"  {sp.propertyPath}: {sp.type}");
            }
        }

        var shops = Object.FindObjectsByType<Shop>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        sb.AppendLine($"\n--- Shop ({shops.Length}) ---");
        foreach (var sh in shops)
        {
            var so = new SerializedObject(sh);
            var sp = so.GetIterator();
            while (sp.NextVisible(true))
            {
                if (sp.propertyType == SerializedPropertyType.ObjectReference && sp.objectReferenceValue != null)
                {
                    sb.AppendLine($"  {sp.propertyPath}: {sp.objectReferenceValue.name} ({sp.objectReferenceValue.GetType().Name})");
                }
            }
        }

        File.WriteAllText("Temp/SwitcherDump.txt", sb.ToString());
        Debug.Log("[CheckSwitcher] Dump complete to Temp/SwitcherDump.txt");
    }

    private static string GetPath(Transform t)
    {
        if (t.parent == null) return t.name;
        return GetPath(t.parent) + "/" + t.name;
    }
}
#endif
