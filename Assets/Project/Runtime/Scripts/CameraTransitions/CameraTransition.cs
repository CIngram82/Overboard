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
    [SerializeField]  TextMeshProUGUI playerPrompt;
    bool canTransition;
    bool hasBeenPrompted;
    bool isMain;

    private void Start()
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
            hasBeenPrompted = true;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!hasBeenPrompted)
    //    {
    //        playerPrompt.text = "Press E to toggle camera";
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (!hasBeenPrompted)
        {
            playerPrompt.text = "Press E to toggle camera";
        }
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            canTransition = true;
            CameraEntered?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerPrompt.text = " ";
        canTransition = false;
    }

    public void SwitchCameras()
    {
        if (isMain)
        {
            transitionCamera.Priority = 2;
            mainCam.Priority = startingPriority;
            isMain = false;
            CameraController.SetCursorLockMode(true);
            camPointer.SetActive(false);
            PlayerMovement.canMove = false;
            GameManager.Instance.inPuzzleView = true;
        }
        else
        {
            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 2;
            isMain = true;
            CameraController.SetCursorLockMode(false);
            camPointer.SetActive(true);
            PlayerMovement.canMove = true;
            GameManager.Instance.inPuzzleView = false;
        }
    }
}
