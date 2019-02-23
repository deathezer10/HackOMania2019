using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MicrophoneButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite m_OnPressedSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = m_OnPressedSprite;
        Microphone.Start("", false, 0, 44100);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Microphone.End("");
    }

}
