using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public static List<GameObject> collectablesList = new List<GameObject>();
    [SerializeField] LayerMask collectables;

    private void Update()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 50, collectables) && Input.GetMouseButtonDown(0))
        {
            collectablesList.Add(rayHit.transform.gameObject);
            rayHit.transform.gameObject.SetActive(false);
        }
    }
}
