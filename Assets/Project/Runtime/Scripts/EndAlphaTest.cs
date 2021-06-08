using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAlphaTest : MonoBehaviour
{

   [SerializeField] GameObject endpanel;

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            endpanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
