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
    [SerializeField]  TextMeshProUGUI playerPrompt;
    bool canTransition;
    bool hasBeenPrompted;
    bool isMain;


    void Start()
    {
        hasBeenPrompted = false;
        startingPriority = transitionCamera.Priority;
        isMain = true;
        canTransition = false;
    }
    private void Update()
    {
        if(canTransition && Input.GetKeyUp(KeyCode.E))
        {
            playerPrompt.text = " ";
            SwitchCameras();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasBeenPrompted)
        {
            playerPrompt.text = "Press E to toggle camera";
        }
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SwitchCameras();
            CameraEntered?.Invoke(other.gameObject);
            hasBeenPrompted = true;
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
