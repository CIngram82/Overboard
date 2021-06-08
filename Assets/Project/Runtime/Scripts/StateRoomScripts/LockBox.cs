using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] List<GameObject> collectablesRequired = new List<GameObject>();
   

    private void OnTriggerStay(Collider other)
    {
       
        if (Input.GetMouseButton(0) && other.gameObject.CompareTag("Player") && checkCollections() == true)
        {
            GrabObject.collectablesList.Clear(); // you probably wouldn't hold these items after use. 
            key.SetActive(true);
        }

    }


    bool checkCollections()
    {
        foreach (GameObject collectables in collectablesRequired)
        {
            if (!GrabObject.collectablesList.Contains(collectables))
            {
               // Debug.Log( collectables.name + "This isnt in the collectionsList");
                return false;
            }

        }
        return true;

    }

}
