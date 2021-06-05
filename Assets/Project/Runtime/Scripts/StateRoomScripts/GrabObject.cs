using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public static List<GameObject> collectablesList = new List<GameObject>();
    [SerializeField] LayerMask collectables;

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined; // this needs to be changed on the camera script when I update that branch and taken out of this one, but the cursor leaving the screen was causing a lot of issues with double monitor.
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 20f, collectables) && Input.GetMouseButtonDown(0))
        {
            collectablesList.Add(rayHit.transform.gameObject);// this only takes the gameObject of the maguffin within the prefab not the entire prefab as that would require restructuring of the world item prefab to include the layer at base.
            rayHit.transform.gameObject.SetActive(false);
        }
       
    }
}
