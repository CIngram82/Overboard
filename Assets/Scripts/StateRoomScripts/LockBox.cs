using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    [SerializeField] GameObject key;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && other.gameObject.CompareTag("Player") && GrabObject.collectablesList.Count >= 3)
        {
            key.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && other.gameObject.CompareTag("Player") && GrabObject.collectablesList.Count >= 3)
        {
            key.SetActive(true);
        }
    }

}
