using System;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTransition : MonoBehaviour
{
    public Action CameraEntered;

    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera transitionCamera;
    [SerializeField] int startingPriority;
    [SerializeField]  TextMeshProUGUI playerPrompt;
    bool hasBeenPrompted;
    bool isMain;
    bool locked;


    void Start()
    {
        locked = false;
        hasBeenPrompted = false;
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!hasBeenPrompted)
        {
            //playerPrompt.text = "Press E to toggle camera";
        }
        if (!locked && Input.GetKeyDown(KeyCode.E))
        {
            SwitchCameras();
            CameraEntered?.Invoke();
            hasBeenPrompted = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        playerPrompt.text = string.Empty;
    }

    public void LockCamera(bool locked)
    {
        this.locked = locked;
        SwitchCameras();
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
