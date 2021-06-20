using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject key;
    public bool isComplete = false;
    /*
    private void OnTriggerStay(Collider other)
    {

        if (Input.GetMouseButton(0) && other.gameObject.CompareTag("Player") && GrabObject.collectablesList.Contains(key))
        {
            GrabObject.collectablesList.Clear();  // assuming you left the key in the door
            isComplete = true;
            gameObject.SetActive(false); // placeholder for door opening
            
        }
    }
    */
}
