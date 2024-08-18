using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingData", menuName = "Data/New EndingData")]
[System.Serializable]
public class EndingData : ScriptableObject
{
    public int SceneID;
    public string Title;
    public int ChooseID;
    public int GoBackID;

}
