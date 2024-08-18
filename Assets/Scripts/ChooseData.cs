using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/New ChooseData")]
[System.Serializable]
public class ChooseData : ScriptableObject
{
    public int ChooseID;
    public int CurIndex;
    public int NeedIndex;
    public int SceneID;
    public int EndSceneID;

    public bool IsCurIndexEqualToNeedIndex()
    {
        return CurIndex == NeedIndex;
    }
}
