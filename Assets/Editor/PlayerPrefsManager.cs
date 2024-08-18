using UnityEngine;
using System.Collections.Generic;

public static class PlayerPrefsManager
{
    private static HashSet<string> playerPrefsKeys = new HashSet<string>();

    // Call this method whenever you set or update a PlayerPrefs value
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
        playerPrefsKeys.Add(key);
    }

    // Call this method whenever you remove a PlayerPrefs value
    public static void RemoveKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
        playerPrefsKeys.Remove(key);
    }

    // Call this method to list all keys
    public static IEnumerable<string> GetAllKeys()
    {
        return playerPrefsKeys;
    }

    // Call this method to clear all PlayerPrefs and keys list
    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        playerPrefsKeys.Clear();
    }
}
