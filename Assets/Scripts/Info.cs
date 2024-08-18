using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public TMP_InputField key;
    public TextMeshProUGUI value;
    private bool keyExists;

    public TMP_InputField CurSceneID;
    public TMP_InputField NextSceneID;

    public ChooseController chooseController;
    public AutoNextVideo controller;

    private void Start()
    {
        CurSceneID.text = controller.GetCurScene();
        NextSceneID.text = controller.GetnextScene();
    }

    private void Update()
    {
        CurSceneID.text = controller.GetCurScene();
        NextSceneID.text = controller.GetnextScene();
    }

    public void NextScene()
    {
        controller.NextScene();
    }

    public void searchPlayerPrefs()
    {
        keyExists = PlayerPrefs.HasKey(key.text);
        if (keyExists)
        {
            value.text = PlayerPrefs.GetInt(key.text).ToString();
        }
        else value.text = "Unknow!";
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(key.text);
        PlayerPrefs.Save();
        
        chooseController.ResetCurIndex(int.Parse(key.text.Substring(key.text.LastIndexOf('_') + 1)));
        value.text = "Done!";
    }
}

