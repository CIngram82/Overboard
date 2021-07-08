using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFeedback : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerFeedbackText;
    public string promptMessage;
    bool isInJournal;

    private void Start()
    {
        isInJournal = false;
    }
    private void OnMouseDown()
    {
        if(!isInJournal)
        {
            StartCoroutine(FeedbackTimer());
            isInJournal = true;
        }
    }
    private void OnMouseExit()
    {
        StopAllCoroutines();
        playerFeedbackText.text = string.Empty;
    }


    IEnumerator FeedbackTimer() // to make sure text doesnt hang out forever 
    {
        playerFeedbackText.text = promptMessage;
        yield return new WaitForSeconds(5);
        playerFeedbackText.text = string.Empty;
    }

}
