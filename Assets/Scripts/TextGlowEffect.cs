using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Security.Cryptography;

public class TextGlowEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public AutoNextVideo controller;

    void Start()
    {
        // Thiết lập màu sắc ban đầu của văn bản
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

    }

    // Sự kiện khi di chuột vào
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = text.fontSize * 1.2f;
    }

    // Sự kiện khi di chuột ra khỏi
    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = text.fontSize / 1.2f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.ChooseEnter(text.text);
    }


}
