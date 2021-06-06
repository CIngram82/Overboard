using UnityEngine;
using Inventory.Collectable;

public class ClickPickupObject : MonoBehaviour
{
    [Header("RayCasting")]
    [SerializeField] LayerMask collectables;
    [SerializeField] float maxDistance = 50.0f;
    [Header("Debugging")]
    [SerializeField] bool debuggingOn = true;
    [SerializeField] Color rayColor = Color.green;

    Camera _rayCamera;
    Ray _ray;


    public void ToggleCursorLockMode()
    {
        Cursor.lockState = (Cursor.lockState != CursorLockMode.Confined) ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void DrawRay()
    {
        _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * maxDistance, rayColor);
        //Debug.LogWarning($"_ray.origin: {_ray.origin}\n_ray.direction: {_ray.direction}");
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (debuggingOn) DrawRay();
#endif
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out RaycastHit rayHit, maxDistance, collectables))
            {
                rayHit.transform.gameObject.GetComponentInParent<WorldItem>().PickUpItem(gameObject);
                rayHit.transform.gameObject.SetActive(false);
            }
        }
    }
    private void Awake()
    {
        _rayCamera = GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        // this needs to be changed on the camera script when I update that branch and taken out of this one,
        // but the cursor leaving the screen was causing a lot of issues with double monitor.
        ToggleCursorLockMode();
    }
}
