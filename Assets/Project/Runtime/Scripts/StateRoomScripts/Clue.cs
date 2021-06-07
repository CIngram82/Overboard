using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clue : MonoBehaviour
{
    [SerializeField] string Message;
    [SerializeField] TextMeshProUGUI clueText;
    [SerializeField] GameObject paperPanel;

    private void OnDisable()
    {
        GrabObject.collectablesList.Remove(gameObject); // this is here for now assuming the paper is being put back down. Can be taken out to be left for use in inventory until puzzle ends if needed.
        clueText.text = Message;
        if (paperPanel != null)
        {
            paperPanel.SetActive(true);
        }
    }
}
