#if UNITY_EDITOR
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class InspectSawAndChain
{
    private const string TRIGGER_FILE = "Temp/RunInspectSawAndChain.txt";
    public const string RESULT_FILE = "Temp/InspectSawAndChainResult.txt";

    static InspectSawAndChain()
    {
        EditorApplication.update += () =>
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            if (!EditorApplication.isPlaying && File.Exists(TRIGGER_FILE))
            {
                try { File.Delete(TRIGGER_FILE); } catch {}
                Debug.Log("[InspectSaw] Trigger detected, opening GamePlay and entering Play Mode...");
                EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
                EditorApplication.isPlaying = true;
            }
        };
    }
}

public class InspectSawAndChainRunner : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnSceneLoaded()
    {
        if (File.Exists("Temp/RunningInspectSaw.txt"))
        {
            try { File.Delete("Temp/RunningInspectSaw.txt"); } catch {}
            var go = new GameObject("InspectSawAndChainRunner");
            go.AddComponent<InspectSawAndChainRunner>();
            Object.DontDestroyOnLoad(go);
        }
    }

    private IEnumerator Start()
    {
        Application.runInBackground = true;
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.5f);

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("=== INSPECT SAW AND CHAIN REPORT ===");

        // 1. Find Stick and Head
        Upgrade up = Object.FindAnyObjectByType<Upgrade>();
        GameObject stick = null;
        if (up != null)
        {
            var f = up.GetType().GetField("_currentStick", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (f != null) stick = f.GetValue(up) as GameObject;
        }

        sb.AppendLine($"Stick found: {stick != null} (name: {stick?.name})");
        if (stick != null)
        {
            sb.AppendLine($"Stick parent: {stick.transform.parent?.name}, pos: {stick.transform.position}, localPos: {stick.transform.localPosition}, scale: {stick.transform.localScale}");
            
            // Print all children of stick
            sb.AppendLine("Stick children hierarchy:");
            PrintHierarchy(stick.transform, sb, 1);
        }

        // 2. Find Head
        Head head = Object.FindAnyObjectByType<Head>();
        sb.AppendLine($"\nHead script found: {head != null}");
        if (head != null)
        {
            sb.AppendLine($"Head GO: {head.gameObject.name}, activeInHierarchy: {head.gameObject.activeInHierarchy}");
            sb.AppendLine($"Head worldPos: {head.transform.position}, localPos: {head.transform.localPosition}, lossyScale: {head.transform.lossyScale}");
            
            // Check Head Rigidbody and velocity
            Rigidbody headRb = head.GetComponent<Rigidbody>();
            if (headRb != null)
            {
                sb.AppendLine($"Head Rigidbody: mass={headRb.mass}, isKinematic={headRb.isKinematic}, velocity={headRb.linearVelocity}, angularVel={headRb.angularVelocity}, constraints={headRb.constraints}");
            }

            // Print Head children and renderers
            sb.AppendLine("Head children hierarchy:");
            PrintHierarchy(head.transform, sb, 1);
        }

        // 3. Find SawPOsitions in scene
        GameObject sawPositions = GameObject.Find("SawPOsitions") ?? GameObject.Find("SawPositions");
        sb.AppendLine($"\nSawPositions found: {sawPositions != null}");
        if (sawPositions != null)
        {
            sb.AppendLine($"SawPositions parent: {sawPositions.transform.parent?.name}, active: {sawPositions.activeInHierarchy}");
            PrintHierarchy(sawPositions.transform, sb, 1);
        }

        // 4. Check Camera view
        Camera cam = Camera.main;
        sb.AppendLine($"\nMain Camera: {cam != null} (pos: {cam?.transform.position}, rot: {cam?.transform.eulerAngles})");
        if (cam != null && head != null)
        {
            Vector3 vp = cam.WorldToViewportPoint(head.transform.position);
            sb.AppendLine($"Head viewport pos relative to Camera: {vp}");
        }

        // 5. Check Control and Joystick input values with NO input
        Control ctrl = Object.FindAnyObjectByType<Control>();
        Joystick joy = Object.FindAnyObjectByType<Joystick>();
        DynamicJoystick djoy = Object.FindAnyObjectByType<DynamicJoystick>();
        sb.AppendLine($"\nControl found: {ctrl != null}");
        sb.AppendLine($"Joystick found: {joy != null} (direction: {joy?.Direction}, H: {joy?.Horizontal}, V: {joy?.Vertical})");
        sb.AppendLine($"DynamicJoystick found: {djoy != null} (direction: {djoy?.Direction})");

        // 6. Monitor velocity of Head and chain over 10 physics frames without player input
        sb.AppendLine("\nMonitoring physics over 10 FixedUpdate ticks (NO INPUT):");
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForFixedUpdate();
            if (head != null)
            {
                Rigidbody rb = head.GetComponent<Rigidbody>();
                sb.AppendLine($"Tick {i}: Head pos={head.transform.position}, vel={rb?.linearVelocity}, joyDir={joy?.Direction}");
            }
        }

        File.WriteAllText(InspectSawAndChain.RESULT_FILE, sb.ToString());
        Debug.Log(sb.ToString());

        yield return new WaitForSecondsRealtime(0.5f);
        EditorApplication.isPlaying = false;
    }

    private void PrintHierarchy(Transform t, System.Text.StringBuilder sb, int indent)
    {
        string prefix = new string(' ', indent * 2);
        Renderer r = t.GetComponent<Renderer>();
        string rendInfo = r != null ? $" [Rend: enabled={r.enabled}, bounds={r.bounds.size}, mat={r.sharedMaterial?.name}]" : "";
        sb.AppendLine($"{prefix}- {t.name} (active={t.gameObject.activeSelf}, inHier={t.gameObject.activeInHierarchy}, localPos={t.transform.localPosition}, scale={t.transform.localScale}){rendInfo}");
        for (int i = 0; i < t.childCount; i++)
        {
            PrintHierarchy(t.GetChild(i), sb, indent + 1);
        }
    }
}
#endif
