using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaTesting : MonoBehaviour
{


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClickOK()
    {
        SceneManager.LoadScene("Alphalevel2");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
