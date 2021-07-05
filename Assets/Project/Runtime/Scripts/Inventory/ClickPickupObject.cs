using UnityEngine;
using Inventory.Collectable;

public class ClickPickupObject : MonoBehaviour
{
    [Header("RayCasting")]
    [SerializeField] LayerMask keyLayer;
    [SerializeField] LayerMask objectLayer;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] LayerMask clueLayer;
    [SerializeField] LayerMask triggerLayer;
    [SerializeField] float maxDistance = 50.0f;
    [Header("Debugging")]
    [SerializeField] bool debuggingOn = true;
    [SerializeField] Color rayColor = Color.green;

    InspectObject inspect;
    Camera _rayCamera;
    Ray _ray;


    public void DrawRay()
    {
        _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * maxDistance, rayColor);
        Debug.DrawRay(_ray.origin, _rayCamera.transform.forward * maxDistance, Color.red);
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (debuggingOn) DrawRay();
#endif
        if (Input.GetMouseButtonDown(0))
            if (InspectObject.IsInspecting)
            {
                _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer)) return;
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, triggerLayer))
                {
                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().PlayAnimation();

                    /*
                    // WorldItem is on root parent containing gameObject of hit collider. 
                    rayHit.transform.gameObject.GetComponentInParent<WorldItem>().PickUpItem(gameObject);
                    AudioScript._instance.PlaySoundEffect("Grab");
                    */
                }
            }
            else
            {
                RaycastHit rayHit;
                _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out rayHit, maxDistance, itemLayer))
                {
                    // WorldItem is on root parent containing gameObject of hit collider. 
                    inspect.Inspect(rayHit.transform.gameObject);
                    rayHit.transform.gameObject.GetComponent<WorldItem>().PickUpItem(gameObject);
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer))
                {
                    inspect.Inspect(rayHit.transform.parent.gameObject);
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, clueLayer))
                {
                    // WorldClue is on root parent containing gameObject of hit collider. 
                    rayHit.transform.gameObject.GetComponentInParent<WorldClue>().PickUpClue(gameObject);
                    //AudioScript._instance.PlaySoundEffect("Grab"); // paper sound
                }
            }
    }

    void Start()
    {
        _rayCamera = CameraController.Camera;
        inspect = GetComponent<InspectObject>();
    }
}
