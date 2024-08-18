using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseController : MonoBehaviour
{
    [SerializeField]
    private List<ChooseData> chooseDataList;

    private Dictionary<int, ChooseData> chooseDataDict;

    private Dictionary<int, int> initialCurIndexes;

    private void Start()
    {
        // Initialize the dictionary
        chooseDataDict = new Dictionary<int, ChooseData>();
        initialCurIndexes = new Dictionary<int, int>();

        // Populate the dictionary
        foreach (var chooseData in chooseDataList)
        {
            chooseDataDict[chooseData.ChooseID] = chooseData;

            initialCurIndexes[chooseData.ChooseID] = chooseData.CurIndex;

            // Optionally load the CurIndex from PlayerPrefs
            string key = "CurIndex_" + chooseData.ChooseID;
            if (PlayerPrefs.HasKey(key))
            {
                chooseData.CurIndex = PlayerPrefs.GetInt(key);
            }
            else
            {
                PlayerPrefs.SetInt(key, chooseData.CurIndex);
                PlayerPrefs.Save();
            }

        }
    }

    // Method to get a ChooseData by ChooseID
    public ChooseData GetChooseDataByID(int chooseID)
    {
        if (chooseDataDict.TryGetValue(chooseID, out var chooseData))
        {
            return chooseData;
        }

        // Handle case where the ID doesn't exist
        Debug.LogWarning($"ChooseData with ID {chooseID} not found!");
        return null;
    }

    public void UpdateCurIndex(int chooseID)
    {
        if (chooseDataDict.TryGetValue(chooseID, out var chooseData))
        {
            chooseData.CurIndex++;
            string key = "CurIndex_" + chooseID;
            
            PlayerPrefs.SetInt(key, chooseData.CurIndex);
            PlayerPrefs.Save();
            Debug.Log($"Updated CurIndex of ChooseData with ID {chooseID} to {chooseData.CurIndex} and saved to PlayerPrefs.");
        }
        else
        {
            Debug.LogWarning($"ChooseData with ID {chooseID} not found!");
        }
    }

    public void ResetCurIndex(int chooseID) 
    {
        if (chooseDataDict.TryGetValue(chooseID, out var chooseData))
        {
            chooseData.CurIndex = 0;
            string key = "CurIndex_" + chooseID;
            PlayerPrefs.SetInt(key, chooseData.CurIndex);
            PlayerPrefs.Save();
            Debug.Log($"Updated CurIndex of ChooseData with ID {chooseID} to {chooseData.CurIndex} and saved to PlayerPrefs.");
        }
        else
        {
            Debug.LogWarning($"ChooseData with ID {chooseID} not found!");
        }
    }

    private void OnDisable()
    {
        // Reset ScriptableObject values to their initial state when the game stops or play mode ends
        foreach (var chooseData in chooseDataList)
        {
            if (initialCurIndexes.TryGetValue(chooseData.ChooseID, out int initialIndex))
            {
                chooseData.CurIndex = initialIndex;
            }
        }
        Debug.Log("ScriptableObject values have been reset to their initial state.");
    }

    // Optional: Manual reset method
    public void ResetScriptableObject()
    {
        foreach (var chooseData in chooseDataList)
        {
            if (initialCurIndexes.TryGetValue(chooseData.ChooseID, out int initialIndex))
            {
                chooseData.CurIndex = initialIndex;
            }
        }
        Debug.Log("ScriptableObject values have been manually reset to their initial state.");
    }

}
