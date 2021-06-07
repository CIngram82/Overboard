using UnityEngine;

public class DropObjectHandler : MonoBehaviour
{
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

    [SerializeField] float snapTolerance = 0.5f;

    void OnDrop(DragObjectHandler dragObject)
    {
        if (!Slot)  // if slot is empty
        {
            float distance = Vector3.Distance(dragObject.transform.position, transform.position);

            Debug.LogError($"{name} distance: {distance}");

            if (distance <= snapTolerance)
            {
                dragObject.transform.SetParent(transform);
                dragObject.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            //dragObject.transform.localPosition = Vector3.zero;
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
