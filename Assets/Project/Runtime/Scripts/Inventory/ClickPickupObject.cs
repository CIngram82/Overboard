using UnityEngine;
using Inventory.Collectable;
using System.Collections.Generic;

public class ClickPickupObject : MonoBehaviour
{
    [Header("RayCasting")]
    [SerializeField] LayerMask items;
    [SerializeField] LayerMask clues;
    [SerializeField] float maxDistance = 50.0f;
    [Header("Debugging")]
    [SerializeField] bool debuggingOn = true;
    [SerializeField] Color rayColor = Color.green;
    public static List<GameObject> collectedItems = new List<GameObject>();
    Camera _rayCamera;
    Ray _ray;


    public void DrawRay()
    {
        _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * maxDistance, rayColor);
        Debug.DrawRay(_ray.origin, _rayCamera.transform.forward * maxDistance, Color.red);
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (debuggingOn) DrawRay();
#endif
        if (Input.GetMouseButtonDown(0) && !InspectObject.IsInspecting)
        {
            RaycastHit rayHit;
            _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out rayHit, maxDistance, items))
            {
                // WorldItem is on root parent containing gameObject of hit collider. 
                rayHit.transform.gameObject.GetComponentInParent<WorldItem>().PickUpItem(gameObject);
                collectedItems.Add(rayHit.collider.gameObject);
            }
            else
            if (Physics.Raycast(_ray, out rayHit, maxDistance, clues))
            {
                // WorldItem is on root parent containing gameObject of hit collider. 
                rayHit.transform.gameObject.GetComponentInParent<WorldClue>().PickUpClue(gameObject);
            }
        }
    }
    void Awake()
    {
        _rayCamera = Camera.main;
    }
}
