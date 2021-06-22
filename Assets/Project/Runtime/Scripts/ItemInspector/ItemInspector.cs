using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspector : MonoBehaviour
{
    [SerializeField] Camera InspectCamera;
    [Header("Camera Settings")]
    [Min(1)] [SerializeField] float rotationSpeed = 1.0f;
    [Min(10)] [SerializeField] float zoomSpeed = 10.0f;
    [Header("Object")]
    //[SerializeField] GameObject inspectedObject;
    bool isInspecting;
    Vector3 position;
    Vector3 zoom;
    Vector3 camPos;
    private void Start()
    {
        isInspecting = false;
    }

    public void SetItemPosition()
    {
        camPos = InspectCamera.transform.parent.position;
        gameObject.transform.parent.position = camPos;
        isInspecting = true;
    }

    void OnMouseScroll()
    {
        var zoomAmount = Input.mouseScrollDelta.y;
        zoom = gameObject.transform.parent.position;

        if (zoomAmount > 0.0f)
        {
            zoom.z += zoomSpeed * Time.deltaTime;
        }
        else if (zoomAmount < 0.0f)
        {
            zoom.z -= zoomSpeed * Time.deltaTime;
        }
        zoom.z = Mathf.Clamp(zoom.z, camPos.z + 1.5f, camPos.z + 2.5f);
        gameObject.transform.parent.position = zoom;
    }
    void OnMouseDown()
    {
        position = Input.mousePosition;
    }
    void OnMouseDrag()
    {
        var deltaPosition = Input.mousePosition - position;

        var axis = Quaternion.AngleAxis(-90.0f, Vector3.forward) * deltaPosition;
        gameObject.transform.parent.rotation = Quaternion.AngleAxis(deltaPosition.magnitude * rotationSpeed, axis) * gameObject.transform.parent.rotation;
        position = Input.mousePosition;
    }

    void Update()
    {
        if(isInspecting)
        {
            OnMouseScroll();
        }

    }
}





