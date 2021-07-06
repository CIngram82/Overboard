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
    [SerializeField] TextMeshProUGUI playerPrompt;
    bool hasBeenPrompted;
    bool hasTransitioned;
    bool isMain;
    bool locked;


    void Start()
    {
        hasTransitioned = false;
        locked = false;
        hasBeenPrompted = false;
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }
    private void Update()
    {
        if(!hasTransitioned && Input.GetKeyUp(KeyCode.E) && !locked)
        {
            SwitchCameras();
            CameraEntered?.Invoke();
            hasTransitioned = true;
            Invoke("waitForTransition", 2f); // 2 seconds is the time it takes for the camera transition to take place, now the player cannot spam e but must wait until a transition is finished
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!hasBeenPrompted)
        {
            playerPrompt.text = "Press E to toggle camera";
        }
        if (!locked && Input.GetKeyDown(KeyCode.E))
        {
           // SwitchCameras();
           // CameraEntered?.Invoke();
            hasBeenPrompted = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        playerPrompt.text = string.Empty;
        hasBeenPrompted = true;
    }

    public void LockCamera(bool locked)
    {
        this.locked = locked;
        SwitchCameras();
    }

    public void SwitchCameras()
    {

        hasBeenPrompted = true;
        playerPrompt.text = string.Empty;

        if (isMain)
        {
            transitionCamera.Priority = 3;
            mainCam.Priority = startingPriority;
            isMain = false;

            GameManager.Instance.inPuzzleView = true;
            EventsManager.On_Camera_Switched(true);
        }
        else
        {
            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 3;
            isMain = true;

            GameManager.Instance.inPuzzleView = false;
            EventsManager.On_Camera_Switched(false);
        }

    }

    void waitForTransition()
    {
        hasTransitioned = false;
    }
}
