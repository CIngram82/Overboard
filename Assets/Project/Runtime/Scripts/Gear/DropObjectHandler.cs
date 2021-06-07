using System.Collections;
using System.Collections.Generic;
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

    void OnDrop()
    {
        if (!Slot)  // if slot is empty
        {

        }
    }
}





