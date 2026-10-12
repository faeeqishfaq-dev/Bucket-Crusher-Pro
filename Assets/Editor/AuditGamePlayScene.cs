#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class AuditGamePlayScene
{
    static AuditGamePlayScene()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists("Temp/RunAudit.txt"))
            {
                try { File.Delete("Temp/RunAudit.txt"); } catch {}
                Audit();
            }
        };
    }
    [MenuItem("Tools/Audit GamePlay Scene")]
    public static void Audit()
    {
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== AUDIT GAMEPLAY SCENE ===");

        var rootGos = scene.GetRootGameObjects();
        foreach (var go in rootGos)
        {
            AuditTransform(go.transform, "", sb);
        }

        File.WriteAllText("Temp/GamePlayAudit.txt", sb.ToString());
        Debug.Log("[Audit] Audit complete. Output written to Temp/GamePlayAudit.txt");
    }

    private static void AuditTransform(Transform t, string indent, StringBuilder sb)
    {
        var comps = t.GetComponents<Component>();
        sb.Append($"{indent}{t.name} (activeSelf={t.gameObject.activeSelf}) [");
        for (int i = 0; i < comps.Length; i++)
        {
            if (comps[i] == null)
            {
                sb.Append("MISSING_SCRIPT, ");
            }
            else
            {
                sb.Append($"{comps[i].GetType().Name}, ");
            }
        }
        sb.AppendLine("]");

        for (int i = 0; i < t.childCount; i++)
        {
            AuditTransform(t.GetChild(i), indent + "  ", sb);
        }
    }
}
#endif
