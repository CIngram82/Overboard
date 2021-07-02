using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera Camera;

    [SerializeField] GameObject _camPointer;

    static float mouseX;
    static float mouseY;
    float rotSpeed = 1;
    bool lastViewState = false;

    void Awake()
    {
        Camera = Camera ? Camera : Camera.main;
        SetCursorLockMode(lastViewState);
    }
    void LateUpdate()
    {
        if (PlayerMovement.canMove)
        {
            CameraControls();
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            ToggleReticle(!lastViewState);
        }
#endif
    }

    void CameraControls()
    {

        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        gameObject.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

    public static void SetCameraRotation(Vector3 rotation)
    {
        mouseX = rotation.y;
        mouseY = rotation.x;
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
    public void ToggleReticle(bool state)
    {
        if (state != lastViewState)
        {
            SetCursorLockMode(state);
            _camPointer.SetActive(!state);
            PlayerMovement.canMove = !state;
        }

        lastViewState = (state != lastViewState) ? state : !state;
    }

    void On_View_Changed(bool state) => ToggleReticle(state);

    void SubToEvents(bool subscribe)
    {
        EventsManager.CameraSwitched -= On_View_Changed;

        if (subscribe)
        {
            EventsManager.CameraSwitched += On_View_Changed;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
}
