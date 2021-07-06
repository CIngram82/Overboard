using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HighlightText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

   [SerializeField] TextMeshProUGUI journalTitle;
    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        journalTitle.color = Color.black;
    }

    public void highlighting()
    {
        journalTitle.color = Color.white;
    }
}
