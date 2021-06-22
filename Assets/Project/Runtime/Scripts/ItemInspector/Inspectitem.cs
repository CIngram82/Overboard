using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inspectitem : MonoBehaviour
{
    [SerializeField] GameObject camPointer;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] GameObject backOutButton;
    ItemInspector inspector;
    GameObject inspectedObject;
    bool isInspecting;

    private void Start()
    {
        isInspecting = false;
    }
    void Update()
    {

        if (Input.GetKeyUp("1"))
        {
            Debug.Log("1 pressed");
            Inspect(ClickPickupObject.collectedItems, 0);
        }

        if (Input.GetKeyUp("2"))
        {
            Debug.Log("2 pressed");
            Inspect(ClickPickupObject.collectedItems, 1);
        }

        if (Input.GetKeyUp("3"))
        {
            Debug.Log("3 pressed");
            Inspect(ClickPickupObject.collectedItems, 2);
        }

        if (Input.GetKeyUp("6"))
        {
            Debug.Log("6 pressed");
          //note functionality?
        }


    }

    void Inspect(List<GameObject> List, int index)
    {
        
        if(List[index] != null)
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
        if (isInspecting)
        {
            CameraController.SetCursorLockMode(true);
            camPointer.SetActive(false);
            PlayerMovement.canMove = false;
        }
        else
        {
            CameraController.SetCursorLockMode(false);
            camPointer.SetActive(true);
            PlayerMovement.canMove = true;
        }
    }

    public void BackOut()
    {
        inspectedObject.SetActive(false);
        isInspecting = false;
        SwitchCameras();
        backOutButton.SetActive(false);
    }
}
