using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingControler : MonoBehaviour
{
    [SerializeField] Button tryagain;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] GameObject bg;
    [SerializeField] AutoNextVideo Controller;

     void Awake()
    {
        bg.SetActive(false);
    }

    public void EndingSceneStart(int SceneID)
    {
        bg.SetActive(true);
        Debug.Log(SceneID);
        EndingData data = Resources.Load<EndingData>($"EndingScene/{SceneID}");
        if(data != null )
        {
            Title.text = data.Title;
        }

    }

    public void ClickTry()
    {
        bg.SetActive(false);
        Controller.TryAgain();
    }
}
