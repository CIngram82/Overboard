using System;
using UnityEngine;


public class DragObjectHandler : MonoBehaviour
{
    public Action<GameObject> ObjectPickedUp;
    public static Action<GameObject> DragStarted;
    public static Action<GameObject> DragEnded;
    static Camera main;

    bool isDragged = false;
    float _zCoordOffsetDrag = -3.5f;
    float _zCoordOffsetDrop = -1.5f;
    Vector3 startPosMouse;
    Vector3 startPosObject;


    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        return main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        ObjectPickedUp?.Invoke(gameObject);
        DragStarted?.Invoke(gameObject);
        isDragged = true;
        transform.parent = null;

        startPosMouse = GetMouseAsWorldPoint();
        startPosObject = transform.position;
        startPosObject.z = _zCoordOffsetDrag;
    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.position = startPosObject + GetMouseAsWorldPoint() - startPosMouse;
        }
    }

    void OnMouseUp()
    {
        isDragged = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, _zCoordOffsetDrop);
        DragEnded?.Invoke(gameObject);
    }

    void Awake()
    {
        main = main == null ? Camera.main : main;
    }
}
