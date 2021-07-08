using System;
using UnityEngine;

public class DropObjectHandler : MonoBehaviour
{
    public static Action ObjectReceived;

    [SerializeField] float snapTolerance = 0.5f;
    public GameObject Slot
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }


    void OnDrop(GameObject dragObject)
    {
        if (!Slot)  // if slot is empty
        {
            float distance = Vector3.Distance(dragObject.transform.localPosition, transform.localPosition);

            if (distance <= snapTolerance)
            {
                dragObject.transform.SetParent(transform);
                dragObject.transform.localPosition = Vector3.zero;
                ObjectReceived?.Invoke();
            }
        }
    }

    void SubToEvents(bool subscribe)
    {
        DragObjectHandler.DragEnded -= OnDrop;

        if (subscribe)
        {
            DragObjectHandler.DragEnded += OnDrop;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
}
