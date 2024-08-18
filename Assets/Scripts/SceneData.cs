using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[CreateAssetMenu(fileName = "SceneData", menuName = "Data/New SceneData")]
[System.Serializable]
public class SceneData : ScriptableObject
{
    public int SceneID;
    public bool hasSubtitle;
    public int nextSceneID;

    public int ChooseID;
    [System.Serializable]
    public struct ChooseOption
    {
        public string OptionTitle;
        public int SceneIDForOption;
        public bool istrue;
        public Vector2 coords;
    }
    public List<ChooseOption> chooses;
    public bool checkChoose;
    public bool endScene;
    public GameObject minigameOption;

}
