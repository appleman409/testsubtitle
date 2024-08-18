using UnityEngine;
using UnityEditor;
using System.IO;

public class SceneDataEditor : EditorWindow
{
    [MenuItem("Tools/Update Scene Data")]
    public static void ShowWindow()
    {
        GetWindow<SceneDataEditor>("Update Scene Data");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Update All Scene Data"))
        {
            UpdateAllSceneData();
        }
    }

    private void UpdateAllSceneData()
    {
        // Load all SceneData assets from the Resources/Scene folder
        string[] guids = AssetDatabase.FindAssets("t:SceneData", new[] { "Assets/Resources/Scene" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            SceneData sceneData = AssetDatabase.LoadAssetAtPath<SceneData>(path);

            if (sceneData != null)
            {

                // Optionally rename the asset file based on SceneID
                string directory = Path.GetDirectoryName(path);
                string newFileName = $"{sceneData.SceneID}.asset";
                string newPath = Path.Combine(directory, newFileName);

                if (!path.EndsWith(newFileName))
                {
                    AssetDatabase.RenameAsset(path, newFileName);
                }
            }
        }

        // Save all changes to assets
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}