using UnityEngine;

public class CameraController : MonoBehaviour
{
    static GameObject camPointer;

    [SerializeField] GameObject _camPointer;

    float rotSpeed = 1;
    float mouseX;
    float mouseY;


    void Start()
    {
        SetCursorLockMode(false);
        camPointer = _camPointer;
    }
    void LateUpdate()
    {
        if (PlayerMovement.canMove)
        {
            CameraControls();
        }

    }

    void CameraControls()
    {

        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        gameObject.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            SetCursorLockMode(!Cursor.visible);
        } 
#endif
    }

    public void SetCameraRotation(Vector3 rotation)
    {
        transform.localEulerAngles = rotation;
    }
    public Quaternion RotatePlayer()
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
    public static void ToggleReticle(bool state)
    {
        SetCursorLockMode(!state);
        camPointer.SetActive(state);
        PlayerMovement.canMove = state;
    }
}
