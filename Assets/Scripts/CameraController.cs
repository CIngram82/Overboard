using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    float rotSpeed = 1;
    float mouseX;
    float mouseY;
    bool hideMouse;
    [SerializeField] Transform player;

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
        gameObject.transform.rotation = Quaternion.Euler(Mathf.Clamp(mouseY * rotSpeed, -30f, 40f), mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = hideMouse;
            Cursor.lockState = CursorLockMode.None;
            hideMouse = !hideMouse;
        }
    }



}
