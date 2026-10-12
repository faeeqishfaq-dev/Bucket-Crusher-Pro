using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class RemoveAds 
{
    public static void CleanScene()
    {
        Scene scene = EditorSceneManager.OpenScene("Assets/Scenes/GamePlay.unity");
        
        GameObject endScreen = GameObject.Find("EndScreen");
        if (endScreen != null)
        {
            Debug.Log("Destroying EndScreen");
            GameObject.DestroyImmediate(endScreen);
        }

        GameObject ui = GameObject.Find("UI");
        if (ui != null)
        {
            // Find NoOil, NoThanks, RVArea under UI if they exist separately
            Transform[] children = ui.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in children)
            {
                if (t != null && (t.name == "NoOil" || t.name == "NoThanks" || t.name == "RVArea"))
                {
                    Debug.Log("Destroying " + t.name);
                    GameObject.DestroyImmediate(t.gameObject);
                }
            }
        }
        
        EditorSceneManager.SaveScene(scene);
        Debug.Log("Scene saved.");
    }
}
