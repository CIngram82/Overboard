using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] GameObject camPointer;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera gearCam;
    bool isMain;

    private void Start()
    {
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
            gearCam.Priority = 2;
            mainCam.Priority = 1;
            isMain = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            camPointer.SetActive(false);
            PlayerMovement.canMove = false;
        }
        else
        {
            gearCam.Priority = 1;
            mainCam.Priority = 2;
            isMain = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            camPointer.SetActive(true);
            PlayerMovement.canMove = true;
        }
    }
}
