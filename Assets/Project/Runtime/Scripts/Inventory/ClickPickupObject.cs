using UnityEngine;

public class ClickPickupObject : MonoBehaviour
{
    [Header("RayCasting")]
    [SerializeField] LayerMask collectables;
    [SerializeField] float maxDistance = 50.0f;
    [Header("Debugging")]
    [SerializeField] bool debuggingOn = true;
    [SerializeField] Color rayColor = Color.green;


    public void ToggleCursorLockMode()
    {
        Cursor.lockState = (Cursor.lockState != CursorLockMode.Confined) ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void DrawRay()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * maxDistance;
        Debug.DrawRay(transform.position, forward, rayColor);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (debuggingOn) DrawRay(); 
#endif
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rayHit, maxDistance, collectables))
            {
                rayHit.transform.gameObject.GetComponent<WorldItem>().PickUpItem(gameObject);
                rayHit.transform.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        // this needs to be changed on the camera script when I update that branch and taken out of this one,
        // but the cursor leaving the screen was causing a lot of issues with double monitor.
        ToggleCursorLockMode();
    }
}
