using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clue : MonoBehaviour
{
    [TextArea]
    [SerializeField] string Message;
    [SerializeField] TextMeshProUGUI clueText;
    [SerializeField] GameObject paperPanel;


    void OnMouseEnter()
    {
        clueText.text = Message;
        paperPanel.SetActive(true);
    }

    void OnMouseExit()
    {
        paperPanel.SetActive(false);
    }

}
