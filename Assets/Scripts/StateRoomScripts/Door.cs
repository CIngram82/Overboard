using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject key;
    private void OnTriggerStay(Collider other)
    {

        if (Input.GetMouseButtonDown(0) && other.gameObject.CompareTag("Player") && GrabObject.collectablesList.Contains(key))
        {
            GrabObject.collectablesList.Clear();  // So it can be reused for finding cogs, or ay other collectable item without the extra uneeded baggage.
            gameObject.SetActive(false); // placeholder for door opening
        }

    }
}
