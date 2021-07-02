using UnityEngine;

public class InspectObject : MonoBehaviour
{
    public static bool IsInspecting;

    [SerializeField] GameObject backOutButton;

    Inventory.Inventory inventory;
    ItemInspector inspector;
    GameObject inspectedObject;


    void Start()
    {
        IsInspecting = false;
        inventory = GetComponent<Inventory.Inventory>();
    }
    void OnGUI()
    {
        if (PauseController.IsPaused) return;
        if (!IsInspecting)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown)
            {
                //Debug.Log("Detected key code: " + e.keyCode);
                GetKey(e.keyCode);
            }
        }
    }

    void GetKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha1:
                InspectInventory(0);
                break;
            case KeyCode.Alpha2:
                InspectInventory(1);
                break;
            case KeyCode.Alpha3:
                InspectInventory(2);
                break;
            case KeyCode.Alpha4:
                InspectInventory(3);
                break;
            case KeyCode.Alpha5:
                InspectInventory(4);
                break;
            case KeyCode.Alpha6:
                InspectInventory(5);
                break;
            default: break;
        }
    }

    public void Inspect(GameObject itemObject)
    {
        backOutButton.SetActive(true);
        inspectedObject = Instantiate(itemObject);
        inspector = inspectedObject.GetComponentInChildren<ItemInspector>();

        IsInspecting = true;
        inspector.SetItemPosition(IsInspecting);
        EventsManager.On_Camera_Switched(IsInspecting);
    }
    public void InspectInventory(int index)
    {
        if (IsInspecting)
        {
            BackOut();
            return;
        }

        if (index < inventory.Items.Count)
        {
            GameObject itemObject = inventory.Items[index].Prefab;
            Inspect(itemObject);
        }
    }

    public void BackOut()
    {
        Destroy(inspectedObject);
        IsInspecting = false;
        EventsManager.On_Camera_Switched(IsInspecting);
        backOutButton.SetActive(false);
    }
}
