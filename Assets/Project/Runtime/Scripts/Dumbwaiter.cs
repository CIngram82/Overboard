using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Dumbwaiter : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waiterCam;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] GameObject camPointer; 
    void OnTriggerStay(Collider other)
    {
        if(IsComplete() && Input.GetMouseButton(0))
        {
            waiterCam.Priority = 4;
            camPointer.SetActive(false);
            Invoke("PlayNextLevel", 1);
        }
        else
        {
            Debug.Log("Puzzles Not Complete");
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
}
