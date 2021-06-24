using System;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTransition : MonoBehaviour
{
    public Action<GameObject> CameraEntered;

    [SerializeField] GameObject camPointer;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera transitionCamera;
    [SerializeField] int startingPriority;
    [SerializeField] TextMeshPro playerPrompt;

    bool isMain;

    private void Start()
    {
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerPrompt.text = "Press E to toggle camera";
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            SwitchCameras();
            CameraEntered?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerPrompt.text = " ";
    }

    void SwitchCameras()
    {
        if (isMain)
        {
            transitionCamera.Priority = 2;
            mainCam.Priority = startingPriority;
            isMain = false;
            CameraController.SetCursorLockMode(true);
            camPointer.SetActive(false);
            PlayerMovement.canMove = false;
        }
        else
        {
            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 2;
            isMain = true;
            CameraController.SetCursorLockMode(false);
            camPointer.SetActive(true);
            PlayerMovement.canMove = true;
        }
    }
}
