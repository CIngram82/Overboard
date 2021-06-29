using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

public class InspectObject : MonoBehaviour
{
    public static bool IsInspecting;

    [SerializeField] GameObject camPointer;
    [SerializeField] GameObject backOutButton;

    InventoryUI inventory;
    ItemInspector inspector;
    GameObject inspectedObject;


    void Start()
    {
        IsInspecting = false;
        inventory = GetComponent<InventoryUI>();
    }
    void Update()
    {
        if (!IsInspecting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1 pressed");
                Inspect(inventory.UIItems[0].Item.Prefab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("2 pressed");
                Inspect(inventory.UIItems[1].Item.Prefab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3 pressed");
                Inspect(inventory.UIItems[3].Item.Prefab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("6 pressed");
                EventsManager.On_Journal_Opened(true);    // TODO: create a global bool in a controller
            }
        }
    }

    void Inspect(GameObject itemObject)
    {
        backOutButton.SetActive(true);
        inspectedObject = Instantiate(itemObject);
        inspector = inspectedObject.GetComponentInChildren<ItemInspector>();
        inspector.SetItemPosition(IsInspecting = true);
        SwitchCameras();
    }

    void SwitchCameras()
    {
        CameraController.SetCursorLockMode(IsInspecting);
        camPointer.SetActive(!IsInspecting);
        PlayerMovement.canMove = !IsInspecting;
    }

    public void BackOut()
    {
        Destroy(inspectedObject);
        //inspectedObject.SetActive(false);
        IsInspecting = false;
        SwitchCameras();
        backOutButton.SetActive(false);
    }
}
