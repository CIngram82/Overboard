using System.Collections;
using UnityEngine;
using InventorySystem;
using InventorySystem.Collectable;

public class ClickPickupObject : MonoBehaviour
{
    [Header("RayCasting")]
    [SerializeField] LayerMask keyLayer;
    [SerializeField] LayerMask objectLayer;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] LayerMask clueLayer;
    [SerializeField] LayerMask triggerLayer;
    [SerializeField] LayerMask startLayer;
    [SerializeField] float maxDistance = 50.0f;
    [Header("Debugging")]
    [SerializeField] bool debuggingOn = true;
    [SerializeField] Color rayColor = Color.green;
    [SerializeField] UIGlow uiGlow;

    //Eric Code
    [SerializeField] ClueInventoryUI uiJournal;
    [SerializeField] GameObject flashlight;

    Inventory _inventory;
    InspectObject _inspect;
    Camera _rayCamera;
    Ray _ray;
    WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);


    public IEnumerator WaitAndPickUp(GameObject objToPickup)     //Allows FindKeyAnimation to play
    {
        Debug.Log($"{objToPickup.name}: Wait");
        yield return waitForSeconds;
        Debug.Log($"{objToPickup.name}: Pickup");

        objToPickup.GetComponent<WorldItem>().PickUpItem(_inventory.gameObject);
        AudioScript._instance.PlaySoundEffect("Grab");
        // Gets item from root parent
        _inventory.RemoveItem(objToPickup.transform.root.GetComponent<WorldItem>().Item);
    }

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
        {
            if (InspectObject.IsInspecting)
            {
                _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer)) return;
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, triggerLayer))
                {
                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().Play();
                    Debug.Log($"{rayHit.transform.gameObject}: Animation Trigger");
                }
                if (Physics.Raycast(_ray, out rayHit, maxDistance, keyLayer))
                {
                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().PlayKey();
                    Debug.Log($"{rayHit.transform.gameObject}: Animation Trigger");
                    StartCoroutine(WaitAndPickUp(rayHit.transform.gameObject));
                }
            }
            else
            {
                RaycastHit rayHit;
                _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out rayHit, maxDistance, clueLayer))          //Looking for World Clues
                {
                    // WorldClue is on root parent containing gameObject of hit collider. 
                    rayHit.transform.gameObject.GetComponentInParent<WorldClue>().PickUpClue(gameObject);
                    uiGlow.DisplayJournalFeedback();
                }
                if (Physics.Raycast(_ray, out rayHit, maxDistance, itemLayer))          //Looking for World Items
                {
                    // WorldItem is on root parent containing gameObject of hit collider. 
                    _inspect.Inspect(rayHit.transform.gameObject);
                    rayHit.transform.gameObject.GetComponent<WorldItem>().PickUpItem(gameObject);
                   
                    uiGlow.AddBackdrop(_inventory.Items.Count - 1);
                    Debug.Log($"InventoryCount: {_inventory.Items.Count}");
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer))        //Looking for inspectable items
                {
                    _inspect.Inspect(rayHit.transform.parent.gameObject);
                    AudioScript._instance.PlaySoundEffect("Grab");
                }
                else
                if (Physics.Raycast(_ray, out rayHit, maxDistance, startLayer))        //Looking for inspectable start items
                {
                    //Eric Code
                    if (rayHit.transform.gameObject.CompareTag("Journal"))
                    {
                        print("Found Journal");
                        rayHit.transform.parent.gameObject.SetActive(false);
                        uiJournal.ActivateJournal();
                        return;
                    }
                    else
                    if (rayHit.transform.gameObject.CompareTag("Flashlight"))
                    {
                        print("Found Flashlight");
                        rayHit.transform.parent.gameObject.SetActive(false);
                        flashlight.SetActive(true);
                    }
                }
            }
        }
    }

    void Start()
    {
        uiGlow = UIGlow.Instance;
        _inventory = Player.inventory;
        _rayCamera = CameraController.Camera;
        _inspect = Player.inspect;
    }
}
