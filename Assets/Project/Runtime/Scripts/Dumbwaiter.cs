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
            //camPointer.SetActive(false);
            //waiterCam.Priority = 4;
            //Invoke("PlayNextLevel", 2);
            SceneManager.LoadScene("CutScene");
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

    }
}
