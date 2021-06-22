using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotSpeed = 1;
    float mouseX;
    float mouseY;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        if (PlayerMovement.canMove)
        {
            CameraControls();
        }
        else
        {
            //Debug.Log(PlayerMovement.canMove);
        }
    }

    void CameraControls()
    {
        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        gameObject.transform.rotation = Quaternion.Euler(Mathf.Clamp(mouseY * rotSpeed, -30f, 40f), mouseX, 0);
        
        if (Input.GetMouseButtonDown(1))
        {
            SetCursorLockMode(!Cursor.visible);
        }
    }

    public Quaternion Rotate()
    {
        return Quaternion.Euler(0, mouseX, 0);
    }

    /// <summary>
    /// Toggles Cursor.lockState between Locked and Confined
    /// and sets Cursor.visible true if not locked.
    /// </summary>
    public static void SetCursorLockMode(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}
