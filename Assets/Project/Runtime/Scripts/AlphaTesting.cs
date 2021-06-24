using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaTesting : MonoBehaviour
{
    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);
        if(currentScene.name == "CutScene")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
 
    }

    public void OnClickOK()
    {
        SceneManager.LoadScene("BetaLevel2");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene("AlphaScene");
    }
}
