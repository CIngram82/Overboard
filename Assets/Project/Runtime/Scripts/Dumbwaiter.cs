using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class Dumbwaiter : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waiterCam;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] GameObject camPointer;
    [SerializeField] TextMeshProUGUI feedbacktext;
    void OnTriggerStay(Collider other)
    {
        if(IsComplete() && Input.GetMouseButton(0))
        {
            waiterCam.gameObject.SetActive(true);
            waiterCam.Priority = 4;
            camPointer.SetActive(false);
            Invoke("PlayNextLevel", 1);
        }
        else
        {
            Debug.Log("Puzzles Not Complete");
        }

        if (IsComplete())
        {
            feedbacktext.text = " Powered \n Click to enter";
        }
        else
        {
            feedbacktext.text = "No power to the dumbwaiter";
        }
    }

    bool IsComplete()
    {
        return (GameManager.Instance.PipePuzzleCompleted &&
                GameManager.Instance.GearPuzzleCompleted);
    }


    public void PlayNextLevel()
    {
        SaveSystem.Data.SaveDataManager.Instance.ResetSave();
        SceneManager.LoadScene(5);
    }


 

    private void OnTriggerExit(Collider other)
    {
        feedbacktext.text = string.Empty;
    }
}
