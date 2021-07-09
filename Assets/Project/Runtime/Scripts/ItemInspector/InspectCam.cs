using InventorySystem;
using InventorySystem.Collectable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectCam : MonoBehaviour
{
    [SerializeField] LayerMask itemLayer;
    [SerializeField] LayerMask objectLayer;
    [SerializeField] LayerMask keyLayer;
    [SerializeField] LayerMask triggerLayer;
    [SerializeField] float maxDistance = 50.0f;

    Camera _rayCamera;
    ItemInspector currentInspector;
    WaitForSeconds waitForSeconds = new WaitForSeconds(3.0f);
    Inventory _inventory;
    bool isAnimating = false;


    public IEnumerator WaitAndPickUp(GameObject objToPickup)     //Allows FindKeyAnimation to play
    {
        isAnimating = true;
        Debug.Log($"{objToPickup.name}: Wait");
        yield return waitForSeconds;
        Debug.Log($"{objToPickup.name}: Pickup");

        objToPickup.GetComponent<WorldItem>().PickUpItem(_inventory.gameObject);
        UIGlow.Instance.AddBackdrop(_inventory.Items.Count - 2);
        AudioScript._instance.PlaySoundEffect("Grab");
        isAnimating = false;
        // Gets item from root parent
        _inventory.RemoveItem(objToPickup.transform.root.GetComponent<WorldItem>().Item);
    }

    #region Mono
    void LateUpdate()
    {
        if (InspectObject.IsInspecting)
        {
            Ray _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(_ray, out rayHit, maxDistance))
            {
                if (rayHit.collider.gameObject.TryGetComponent(out ItemInspector inspector))
                {
                    if (currentInspector != inspector)
                    {
                        currentInspector = inspector;
                    }
                }
            }
            if (!currentInspector) return;
            if (Input.GetMouseButtonDown(0))
                currentInspector.OnMouseDown();
            if (Input.GetMouseButtonUp(0) && !isAnimating &&
                Physics.Raycast(_ray, out rayHit, maxDistance, objectLayer))
            {
                if (triggerLayer == (triggerLayer | (1 << rayHit.transform.gameObject.layer)))
                {
                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().Play();
                    Debug.Log($"{rayHit.transform.gameObject}: Animation Trigger");
                }
                else
                if (keyLayer == (keyLayer | (1 << rayHit.transform.gameObject.layer)))
                {
                    rayHit.transform.gameObject.GetComponent<AnimationTrigger>().PlayKey();
                    Debug.Log($"{rayHit.transform.gameObject}: Animation Trigger");
                    StartCoroutine(WaitAndPickUp(rayHit.transform.gameObject));
                }
            }
            if (Input.GetMouseButton(0))
                currentInspector.OnMouseDrag();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (var item in Player.inventory.Items)
            {
                Debug.LogError($"Item: {item.Name}");
            }
        }
    }

    void Start()
    {
        _rayCamera = GetComponent<Camera>();
        _inventory = Player.inventory;
    }
    #endregion
}





