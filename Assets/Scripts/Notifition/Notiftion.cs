using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class Notiftion : MonoBehaviour
{
    public TextMeshProUGUI textnotifition;
    public Vector3 LeantweenTo;
    public float speed=4f;
    public float duration=60f;
    public Vector3 beginLocation;

    void Start()
    {
        Debug.Log(gameObject.transform);
        gameObject.transform.position = new Vector3(0, 150, 0);
        beginLocation = gameObject.transform.position;
        RectTransform rect = gameObject.GetComponent<RectTransform>();

        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(-(1920 - textnotifition.preferredWidth), -(textnotifition.preferredHeight +25));
        LeanTween.moveLocalX(gameObject, 100, speed).setEase(LeanTweenType.easeOutQuint).setOnComplete(Stay);
        
    }

    void Stay()
    {
        LeanTween.delayedCall(duration, End);
    }    

    void End()
    {
        LeanTween.move(gameObject, beginLocation, speed).setEase(LeanTweenType.easeInSine);
    }    

}
