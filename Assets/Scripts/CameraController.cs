using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotSpeed = 1;
    float mouseX;
    float mouseY;
    bool hideMouse;
   [SerializeField] Transform Player;
   [SerializeField] Transform forward;

    private void Start()
    {
        hideMouse = true;
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
        mouseY = Mathf.Clamp(mouseY, -35, -60);

        forward.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);

        if(Input.GetMouseButton(1) && hideMouse)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            hideMouse = false;
        }
        else if(Input.GetMouseButton(1) && !hideMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            hideMouse = true;
        }
    }



}
