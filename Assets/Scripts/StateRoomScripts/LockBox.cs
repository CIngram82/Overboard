using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    [SerializeField] GameObject key;

    private void OnTriggerStay(Collider other)
    {
       
        if (Input.GetMouseButton(0) && other.gameObject.CompareTag("Player") && GrabObject.collectablesList.Count >= 3)
        {
            GrabObject.collectablesList.Clear(); // you probably wouldn't hold these items after use. 
            key.SetActive(true);
        }
    }

}
