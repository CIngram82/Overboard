using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragObjectHandler : MonoBehaviour
{
    public delegate void OnDragEndDelegate(DragObjectHandler dragObject);
    public OnDragEndDelegate OnDragEndCallback;

    float _zCoordOffsetUp = -3.5f;
    float _zCoordOffsetDown = -0.5f;


    bool isDragged = false;
    Vector3 startPosMouse;
    Vector3 startPosObject;


    static Camera main;


    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        return main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        isDragged = true;

        startPosObject = transform.localPosition;
        startPosObject.z = _zCoordOffsetUp;
    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.localPosition = startPosObject + GetMouseAsWorldPoint() - startPosMouse;
        }
    }

    void OnMouseUp()
    {
        isDragged = false;
        OnDragEndCallback(this);
    }

    void Awake()
    {
        main = main == null ? Camera.main : main;
    }
}
