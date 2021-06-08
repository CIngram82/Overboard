using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;

    float rotSpeed = 1;
    float mouseX;
    float mouseY;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        CameraControls();
    }

    void CameraControls()
    {
        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        gameObject.transform.rotation = Quaternion.Euler(Mathf.Clamp(mouseY * rotSpeed, -30f, 40f), mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = !Cursor.visible;
            ToggleCursorLockMode();
        }
    }

    public void ToggleCursorLockMode()
    {
        Cursor.lockState = (Cursor.lockState != CursorLockMode.Confined) ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}
