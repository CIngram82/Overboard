using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGlow : MonoBehaviour
{
    public List<GameObject> glowBackList = new List<GameObject>();
    public static bool journalInteracted;
    public static UIGlow Instance;


    public void AddBackdrop(int inventoryIndex)
    {
        StartCoroutine(disableBackdrop(inventoryIndex));
    }

    public IEnumerator disableBackdrop(int index)
    {
        glowBackList[index].SetActive(true);
        Image image = glowBackList[index].GetComponent<Image>();
        Color color = image.color;
        color.a = .0f;

        for (float i = 0; i < .5f; i += .1f)
        {
            color.a += i;
            yield return new WaitForSeconds(.3f);
            image.color = color;
        }

        glowBackList[index].SetActive(false);
        yield return null;
    }

    public IEnumerator JournalFeedback()
    {
        while (!journalInteracted)
        {
            glowBackList[6].SetActive(true);
            Image image = glowBackList[6].GetComponent<Image>();
            Color color = image.color;
            color.a = .0f;
            for (float i = 0; i < .5f; i += .1f)
            {
                color.a += i;
                yield return new WaitForSeconds(.3f);
                image.color = color;
            }
        }
        glowBackList[6].SetActive(false);
        yield return null;
    }

    public void DisplayJournalFeedback()
    {
        DisableJournalFeedback(); // not an accident, no touchey
        journalInteracted = false;
        StartCoroutine(JournalFeedback());
    }

    public void DisableJournalFeedback()
    {
        journalInteracted = true;
        glowBackList[6].SetActive(false);
        StopAllCoroutines();
    }

    void Awake()
    {
        Instance = this;
    }
}
