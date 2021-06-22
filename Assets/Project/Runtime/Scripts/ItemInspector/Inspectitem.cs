using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InspectItem : MonoBehaviour
{
    [SerializeField] GameObject camPointer;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] GameObject backOutButton;

    ItemInspector inspector;
    GameObject inspectedObject;
   public static bool isInspecting;


    private void Start()
    {
        isInspecting = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");
            Inspect(ClickPickupObject.collectedItems, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 pressed");
            Inspect(ClickPickupObject.collectedItems, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3 pressed");
            Inspect(ClickPickupObject.collectedItems, 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("6 pressed");
            EventsManager.On_Journal_Opened(true);    // TODO: create a global bool in a controller
        }
    }

    void Inspect(List<GameObject> List, int index)
    {
        if (List[index] != null)
        {
            backOutButton.SetActive(true);
            isInspecting = true;
            inspectedObject = List[index].transform.parent.gameObject;
            inspectedObject.SetActive(true);
            inspector = List[index].GetComponent<ItemInspector>();
            inspector.SetItemPosition();
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        CameraController.SetCursorLockMode(isInspecting);
        camPointer.SetActive(!isInspecting);
        PlayerMovement.canMove = !isInspecting;
    }

    public void BackOut()
    {
        inspectedObject.SetActive(false);
        isInspecting = false;
        SwitchCameras();
        backOutButton.SetActive(false);
    }
}
