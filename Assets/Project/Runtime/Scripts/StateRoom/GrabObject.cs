using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public static List<GameObject> collectablesList = new List<GameObject>();
    [SerializeField] LayerMask collectables;
    /*
    private void Update()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 20f, collectables) && Input.GetMouseButtonDown(0))
        {
            collectablesList.Add(rayHit.transform.gameObject);// this only takes the gameObject of the maguffin within the prefab not the entire prefab as that would require restructuring of the world item prefab to include the layer at base.
            rayHit.transform.gameObject.SetActive(false);
        }
       
    }
    */
}
