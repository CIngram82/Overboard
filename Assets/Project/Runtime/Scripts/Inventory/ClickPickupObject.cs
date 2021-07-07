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
    [SerializeField] UIGlow uiGlow;

    //Eric Code
    [SerializeField] ClueInventoryUI uiJournal;
    [SerializeField] GameObject flashlight;

    Inventory.Inventory inventory;
    InspectObject inspect;
    [SerializeField] Camera _rayCamera;
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
                //Debug.Log("click while inspecting");
                _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer)) { return; }

                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, triggerLayer))
                {

                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().PlayOpen();
                    Debug.Log("TRIGGERED");
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
                    uiGlow.AddBackdrop(inventory.Items.Count - 1);
                    Debug.Log("InventoryCount: " + inventory.Items.Count);
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer))
                {
                    //Eric Code
                    if (rayHit.transform.gameObject.CompareTag("Journal"))
                    {
                        print("found journal");
                        rayHit.transform.gameObject.SetActive(false);
                        uiJournal.ActivateJournal();
                        return;
                    }
                    if (rayHit.transform.gameObject.CompareTag("Flashlight"))
                    {
                        print("found flashlight");
                        rayHit.transform.parent.gameObject.SetActive(false);
                        flashlight.SetActive(true);
                        return;
                    }

                    inspect.Inspect(rayHit.transform.parent.gameObject);
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, clueLayer))
                {
                    // WorldClue is on root parent containing gameObject of hit collider. 
                    rayHit.transform.gameObject.GetComponentInParent<WorldClue>().PickUpClue(gameObject);
                    uiGlow.AddBackdrop(6);
                    //AudioScript._instance.PlaySoundEffect("Grab"); // paper sound
                }
            }
    }

    void Start()
    {
        uiGlow = FindObjectOfType<UIGlow>();
        inventory = Player.inventory;
        _rayCamera = CameraController.Camera;
        inspect = Player.inspect;
    }
}
