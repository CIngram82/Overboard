using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DialRotator : MonoBehaviour
{

    GameObject selectedGO;
    bool mouseIsDown;
    float mouseDownRotation;
    float previousRotation;
    Vector3 mouseDownPosition;
    Vector3 pistonDownPosition;
    float pistonMovementScale;
    bool canMoveForward;
    bool canMoveBack;
    List<DirectionalTrigger> forwardTriggers = new List<DirectionalTrigger>();
    List<DirectionalTrigger> backTriggers = new List<DirectionalTrigger>();

    [SerializeField] Camera camera;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedGO = RayHit();

            if(selectedGO)
            {
                mouseIsDown = true;
                //mouseDownRotation = previousRotation;
                mouseDownRotation = selectedGO.transform.localRotation.eulerAngles.y;
                //previousRotation = mouseDownRotation;
                print("Previous rotation = " + mouseDownRotation);
                mouseDownPosition = Input.mousePosition;
                pistonDownPosition = selectedGO.transform.localPosition;
                pistonMovementScale = Vector3.Distance(camera.transform.position, selectedGO.transform.position) / Screen.height;

                //make trigger lists for the currently selectedGO 
                FillTriggerLists();

                UpdateTriggerLists();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedGO = RayHit();
            mouseIsDown = false;

            //clear trigger lists
            forwardTriggers.Clear();
            backTriggers.Clear();
        }

        if (mouseIsDown)
        {
            if (selectedGO.CompareTag("safeRing"))
            {
                Vector3 localMousePosition = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
                //float currentRotation = -1 * Mathf.Atan(localMousePosition.x / localMousePosition.y) * Mathf.Rad2Deg;
                float currentRotation = mouseDownRotation - Mathf.Atan(localMousePosition.x / localMousePosition.y) * Mathf.Rad2Deg;
                float deltaRotation = previousRotation - currentRotation;

                

                if (deltaRotation > 0 && canMoveForward)
                {
                    //print(deltaRotation);
                    selectedGO.transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
                }
                else if (deltaRotation < 0 && canMoveBack)
                {
                    //print(deltaRotation);
                    selectedGO.transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
                }

                //Set local rotation for ring based on rotation on mouse down and offset.
                previousRotation = currentRotation;
                //print("Current rotation is: " + previousRotation);
            }
            else if (selectedGO.CompareTag("safePiston"))
            {
                //Set local position for piston based on position on mouse down and offset.
                float sign = -1 * Mathf.Sign(mouseDownPosition.z - Input.mousePosition.z + mouseDownPosition.y - Input.mousePosition.y);

                if(sign > 0 && canMoveForward)
                {
                    selectedGO.transform.localPosition = new Vector3(0f, 0f, pistonDownPosition.z + Vector3.Distance(mouseDownPosition, Input.mousePosition) * pistonMovementScale * sign);
                }
                else if(sign < 0 && canMoveBack)
                {
                    selectedGO.transform.localPosition = new Vector3(0f, 0f, pistonDownPosition.z + Vector3.Distance(mouseDownPosition, Input.mousePosition) * pistonMovementScale * sign);
                }

            }


        }
    }

    private GameObject RayHit()
    {
        GameObject output = null;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //print("ray out");

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("safeRing") || hit.transform.CompareTag("safePiston"))
            {
                output = hit.transform.gameObject;
                Vector3 hitPoint = hit.point;

                Debug.DrawLine(camera.transform.position, hitPoint, Color.white, 2f);
                //Debug.Log("Did Hit");
            }
            else
            {
                Debug.LogError("not a safe ring or piston");
            }
        }

        return output;
    }
    
    public void FillTriggerLists()
    {
        DirectionalTrigger[] triggers = selectedGO.GetComponentsInChildren<DirectionalTrigger>();
        foreach (DirectionalTrigger i in triggers)
        {
            if (i.IsForwardTrigger())
            {
                forwardTriggers.Add(i);
            }
            else
            {
                backTriggers.Add(i);
            }
        }
    }

    public void UpdateTriggerLists()
    {
        canMoveForward = true;
        int numTriggers = 0;
        foreach (DirectionalTrigger i in forwardTriggers)
        {
            numTriggers++;
            if (i.IsOverlapping())
            {
                canMoveForward = false;
            }
        }

        canMoveBack = true;
        foreach (DirectionalTrigger i in backTriggers)
        {
            if (i.IsOverlapping())
            {
                canMoveBack = false;
            }
        }

        //Debug.LogError("F, B, Nf:  " + canMoveForward + ", " + canMoveBack + ", " +numTriggers);
    }
}



