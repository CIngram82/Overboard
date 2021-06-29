using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalTrigger : MonoBehaviour
{
    DialRotator rotator;
    [SerializeField] bool forwardTrigger;
    bool overlapping;

    private void Start()
    {
        rotator = FindObjectOfType<DialRotator>();
        //print("found: " + rotator);
    }

    private void OnTriggerEnter(Collider other)
    {
        overlapping = true;
        //print("forward: " + IsForwardTrigger() + " trigger is overlapping: " + IsOverlapping());
        rotator.UpdateTriggerLists();
    }

    private void OnTriggerExit(Collider other)
    {
        overlapping = false;
        rotator.UpdateTriggerLists();
    }

    public bool IsOverlapping()
    {
        return overlapping;
    }

    public bool IsForwardTrigger()
    {
        return forwardTrigger;
    }
}
