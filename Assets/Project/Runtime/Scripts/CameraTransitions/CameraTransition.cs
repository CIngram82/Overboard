using System;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTransition : MonoBehaviour
{
    public Action<GameObject> CameraEntered;

    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera transitionCamera;
    [SerializeField] int startingPriority;
    [SerializeField] TextMeshPro playerPrompt;

    bool isMain;


    void Start()
    {
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }

    void OnTriggerEnter(Collider other)
    {
        playerPrompt.text = "Press E to toggle Camera.";
    }
    void OnTriggerStay(Collider other)
    {
        if (PauseController.IsPaused) return;
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SwitchCameras();
            CameraEntered?.Invoke(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        playerPrompt.text = string.Empty;
    }

    public void SwitchCameras()
    {
        if (isMain)
        {
            transitionCamera.Priority = 2;
            mainCam.Priority = startingPriority;
            isMain = false;

            GameManager.Instance.inPuzzleView = true;
            EventsManager.On_Camera_Switched(true);
        }
        else
        {
            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 2;
            isMain = true;

            GameManager.Instance.inPuzzleView = false;
            EventsManager.On_Camera_Switched(false);
        }
    }
}
