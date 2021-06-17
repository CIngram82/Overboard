using UnityEngine;
using UnityEngine.SceneManagement;

public class Dumbwaiter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(IsComplete())
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
