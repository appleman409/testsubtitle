using UnityEngine;
using System.Reflection;

public class PlayerPrefsDebugger : MonoBehaviour
{
    private void Start()
    {
        var prefsType = typeof(PlayerPrefs);
        var method = prefsType.GetMethod("GetAllKeys", BindingFlags.NonPublic | BindingFlags.Static);
        if (method != null)
        {
            var allKeys = (string[])method.Invoke(null, null);
            foreach (var key in allKeys)
            {
                Debug.Log(key + ": " + PlayerPrefs.GetString(key));
            }
        }
    }
}
