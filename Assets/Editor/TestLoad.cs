using UnityEditor;
using UnityEngine;
using TMPro;

public class TestLoad
{
    [InitializeOnLoadMethod]
    static void Test()
    {
        var f = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/MonoBehaviour/Rubik-SemiBold SDF.asset");
        if (f == null) Debug.LogError("FAILED TO LOAD FONT ASSET!");
        else Debug.Log("FONT ASSET LOADED PERFECTLY! FaceInfo: " + f.faceInfo.familyName);
    }
}
