#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class InspectStickJoints
{
    static InspectStickJoints()
    {
        EditorApplication.delayCall += Inspect;
    }

    public static void Inspect()
    {
        string[] prefabPaths = new string[]
        {
            "Assets/GameObject/1.prefab",
            "Assets/GameObject/2.prefab",
            "Assets/GameObject/3.prefab",
            "Assets/GameObject/4.prefab",
            "Assets/GameObject/6.prefab"
        };

        StringBuilder sb = new StringBuilder();

        foreach (var path in prefabPaths)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                sb.AppendLine($"Could not load prefab at {path}");
                continue;
            }

            sb.AppendLine($"================== PREFAB: {prefab.name} ({path}) ==================");
            var rbs = prefab.GetComponentsInChildren<Rigidbody>(true);
            sb.AppendLine($"Total Rigidbodies: {rbs.Length}");
            foreach (var rb in rbs)
            {
                sb.AppendLine($"  RB on '{rb.gameObject.name}': isKinematic={rb.isKinematic}, mass={rb.mass}, drag={rb.linearDamping}, useGravity={rb.useGravity}, constraints={rb.constraints}, localPos={rb.transform.localPosition}");
            }

            var joints = prefab.GetComponentsInChildren<Joint>(true);
            sb.AppendLine($"Total Joints: {joints.Length}");
            foreach (var j in joints)
            {
                sb.AppendLine($"  Joint '{j.GetType().Name}' on '{j.gameObject.name}': connectedBody='{(j.connectedBody != null ? j.connectedBody.name : "null")}', anchor={j.anchor}, connectedAnchor={j.connectedAnchor}, autoConfigureConnectedAnchor={j.autoConfigureConnectedAnchor}");
                if (j is ConfigurableJoint cj)
                {
                    sb.AppendLine($"    CJ Config: xMotion={cj.xMotion}, yMotion={cj.yMotion}, zMotion={cj.zMotion}, angXMotion={cj.angularXMotion}, angYMotion={cj.angularYMotion}, angZMotion={cj.angularZMotion}");
                    sb.AppendLine($"    CJ Limits: lowAngX={cj.lowAngularXLimit.limit}, highAngX={cj.highAngularXLimit.limit}, angY={cj.angularYLimit.limit}, angZ={cj.angularZLimit.limit}");
                    sb.AppendLine($"    CJ Drive: xDrive(posSpring={cj.xDrive.positionSpring}, damper={cj.xDrive.positionDamper}), slerpDrive(posSpring={cj.slerpDrive.positionSpring}, damper={cj.slerpDrive.positionDamper})");
                }
            }

            var heads = prefab.GetComponentsInChildren<Head>(true);
            foreach (var h in heads)
            {
                sb.AppendLine($"  Head on '{h.gameObject.name}', RB='{h.GetComponent<Rigidbody>()?.name}', parent='{h.transform.parent?.name}', localPos={h.transform.localPosition}");
            }
        }

        File.WriteAllText("StickJointsLog.txt", sb.ToString());
        Debug.Log("[InspectStickJoints] Wrote StickJointsLog.txt successfully!");
    }
}
#endif
