using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldmasterLevel2 : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
        CameraController.SetCursorLockMode(true);
        SceneManager.LoadScene("Scene_Credits");
    }
}
