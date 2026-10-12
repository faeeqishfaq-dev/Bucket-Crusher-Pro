#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[InitializeOnLoad]
public static class InspectCanvases
{
    static InspectCanvases()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (File.Exists("Temp/RunInspectCanvases.txt"))
            {
                try { File.Delete("Temp/RunInspectCanvases.txt"); } catch {}
                Inspect();
            }
        };
    }

    [MenuItem("Tools/Inspect Canvases")]
    public static void Inspect()
    {
        CleanGamePlayPopupsAndConfigureInput.Execute();
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity", OpenSceneMode.Single);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== INSPECT ALL CANVASES AND POPUPS IN GAMEPLAY ===");

        var canvases = Object.FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var c in canvases)
        {
            var cg = c.GetComponent<CanvasGroup>();
            var gr = c.GetComponent<GraphicRaycaster>();
            sb.AppendLine($"CANVAS: {GetPath(c.transform)} | activeSelf={c.gameObject.activeSelf} activeInHier={c.gameObject.activeInHierarchy} enabled={c.enabled} sortOrder={c.sortingOrder} renderMode={c.renderMode}");
            if (cg != null)
            {
                sb.AppendLine($"   CanvasGroup: alpha={cg.alpha} interactable={cg.interactable} blocksRaycasts={cg.blocksRaycasts} ignoreParent={cg.ignoreParentGroups}");
            }
            if (gr != null)
            {
                sb.AppendLine($"   GraphicRaycaster: enabled={gr.enabled} blockingMask={gr.blockingMask.value} blockingObjects={gr.blockingObjects}");
            }

            // Check children for active images with raycastTarget
            var graphics = c.GetComponentsInChildren<Graphic>(true);
            foreach (var g in graphics)
            {
                if (g.gameObject.activeInHierarchy && g.raycastTarget)
                {
                    var rt = g.rectTransform;
                    sb.AppendLine($"      Graphic: {GetPath(g.transform)} (activeSelf={g.gameObject.activeSelf}, raycastTarget={g.raycastTarget}, size={rt.sizeDelta}, color={g.color})");
                }
            }
        }

        sb.AppendLine("\n=== INPUT AND CONTROLS ===");
        var esList = Object.FindObjectsByType<EventSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        sb.AppendLine($"EventSystems count: {esList.Length}");
        foreach (var es in esList)
        {
            sb.AppendLine($"  EventSystem: {es.name} activeInHier={es.gameObject.activeInHierarchy}");
            foreach (var m in es.GetComponents<BaseInputModule>())
            {
                sb.AppendLine($"    InputModule: {m.GetType().Name} enabled={m.enabled}");
            }
        }

        var inputCanvasGo = GameObject.Find("InputCanvas");
        if (inputCanvasGo != null)
        {
            var c = inputCanvasGo.GetComponent<Canvas>();
            sb.AppendLine($"InputCanvas: activeInHier={inputCanvasGo.activeInHierarchy} renderMode={c.renderMode} sortOrder={c.sortingOrder} worldCam={c.worldCamera?.name}");
            foreach (Transform t in inputCanvasGo.GetComponentsInChildren<Transform>(true))
            {
                var comps = t.GetComponents<Component>();
                var rt = t.GetComponent<RectTransform>();
                sb.Append($"  {t.name} (active={t.gameObject.activeSelf}, rect={rt?.rect}, pos={rt?.anchoredPosition}) [");
                foreach (var comp in comps) if (comp != null) sb.Append($"{comp.GetType().Name}, ");
                sb.AppendLine("]");
            }
        }

        var excavator = GameObject.Find("excavator");
        if (excavator != null)
        {
            var ctrl = excavator.GetComponent<Control>();
            if (ctrl != null)
            {
                var joyField = typeof(Control).GetField("_joy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                sb.AppendLine($"Control._joy: {joyField?.GetValue(ctrl)}");
            }
        }

        File.WriteAllText("Temp/CanvasesInspection.txt", sb.ToString());
        Debug.Log("[InspectCanvases] Done -> Temp/CanvasesInspection.txt");
    }

    private static string GetPath(Transform t)
    {
        if (t.parent == null) return t.name;
        return GetPath(t.parent) + "/" + t.name;
    }
}
#endif
