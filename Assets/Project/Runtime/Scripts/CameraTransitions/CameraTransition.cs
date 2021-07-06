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
    bool canTransition;
    bool isMain;
    bool locked;


    void Start()
    {
        canTransition = false;
        locked = false;
        hasBeenPrompted = false;
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }
    private void Update()
    {
        if(canTransition && Input.GetKeyUp(KeyCode.E) && !locked)
        {
            canTransition = false;
            SwitchCameras();
            CameraEntered?.Invoke();
            Debug.LogError($"{name}, {transitionCamera.name}: triggered");
            Invoke("waitForTransition", 2f); // 2 seconds is the time it takes for the camera transition to take place, now the player cannot spam e but must wait until a transition is finished
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        canTransition = true;
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
        canTransition = false;
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
        canTransition = true;
    }
}
