using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerPrefsEditorWindow : EditorWindow
{
    private string key = "";
    private string value = "";
    private bool keyExists;

    private Vector2 scrollPosition;

    [MenuItem("Window/PlayerPrefs Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsEditorWindow>("PlayerPrefs Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Editor", EditorStyles.boldLabel);

        // Key input field
        key = EditorGUILayout.TextField("Key", key);

        if (GUILayout.Button("Check Key"))
        {
            CheckKey();
        }

        if (keyExists)
        {
            value = EditorGUILayout.TextField("Value", value);

            if (GUILayout.Button("Update Value"))
            {
                UpdateValue();
            }

            if (GUILayout.Button("Delete Key"))
            {
                DeleteKey();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Key does not exist.");
        }

        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            ClearAll();
        }

        // List all keys
        GUILayout.Label("All PlayerPrefs Keys", EditorStyles.boldLabel);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        foreach (var key in PlayerPrefsManager.GetAllKeys())
        {
            EditorGUILayout.LabelField(key, PlayerPrefs.GetString(key));
        }
        EditorGUILayout.EndScrollView();
    }

    private void CheckKey()
    {
        keyExists = PlayerPrefs.HasKey(key);
        if (keyExists)
        {
            value = PlayerPrefs.GetInt(key).ToString();
        }
    }

    private void UpdateValue()
    {
        PlayerPrefsManager.SetString(key, value);
        Debug.Log($"Updated PlayerPrefs key '{key}' to value '{value}'");
    }

    private void DeleteKey()
    {
        PlayerPrefsManager.RemoveKey(key);
        Debug.Log($"Deleted PlayerPrefs key '{key}'");
        keyExists = false;
        value = "";
    }

    private void ClearAll()
    {
        PlayerPrefsManager.ClearAll();
        Debug.Log("Cleared all PlayerPrefs");
        keyExists = false;
        value = "";
    }
}
