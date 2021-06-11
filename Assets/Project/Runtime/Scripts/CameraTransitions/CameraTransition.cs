using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] GameObject camPointer;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera transitionCamera;
    [SerializeField] int startingPriority;
  
    bool isMain;

    private void Start()
    {
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        if (isMain)
        {
            transitionCamera.Priority = 2;
            mainCam.Priority = startingPriority;
            isMain = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            camPointer.SetActive(false);
        }
        else
        {
            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 2;
            isMain = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            camPointer.SetActive(true);
        }
    }
}
