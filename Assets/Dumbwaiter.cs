using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dumbwaiter : MonoBehaviour
{
    PipePuzzle.PipeGameManager pipePuzzle;
    Door door;
   
    private void OnTriggerEnter(Collider other)
    {
        if(isComplete())
        {
            SceneManager.LoadScene("CutScene");
        }
        else
        {
            Debug.Log("Puzzles Not Complete");
        }
    }

    bool isComplete()
    {
        if(pipePuzzle.isComplete && door.isComplete)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
