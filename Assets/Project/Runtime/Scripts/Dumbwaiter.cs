using UnityEngine;
using UnityEngine.SceneManagement;

public class Dumbwaiter : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(IsComplete() && Input.GetMouseButton(0))
        {
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
}
