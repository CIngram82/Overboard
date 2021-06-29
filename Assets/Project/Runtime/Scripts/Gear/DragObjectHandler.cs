using System;
using UnityEngine;

public class DragObjectHandler : MonoBehaviour
{
    public Action<GameObject> ObjectPickedUp;
    public static Action<GameObject> DragStarted;
    public static Action<GameObject> DragEnded;
    static Camera main;

    public bool Enabled = true;
    [Space]
    [SerializeField] float _zCoordOffsetDrag = -3.5f;
    [SerializeField] float _zCoordOffsetDrop = -1.5f;
    
    bool isDragged = false;
    Vector3 startPosMouse;
    Vector3 startPosObject;

    public static Transform Parent { get; set; }


    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        return main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        if (!Enabled) return;

        ObjectPickedUp?.Invoke(gameObject);
        DragStarted?.Invoke(gameObject);
        isDragged = true;
        transform.parent = Parent;

        startPosMouse = GetMouseAsWorldPoint();
        startPosObject = transform.localPosition;
        startPosObject.z = _zCoordOffsetDrag;
    }

    void OnMouseDrag()
    {
        if (!Enabled) return;

        if (isDragged)
        {
            transform.localPosition = startPosObject + GetMouseAsWorldPoint() - startPosMouse;
        }
    }

    void OnMouseUp()
    {
        if (!Enabled) return;

        isDragged = false;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _zCoordOffsetDrop);
        DragEnded?.Invoke(gameObject);
    }

    void Awake()
    {
        main = main == null ? Camera.main : main;
    }
}
