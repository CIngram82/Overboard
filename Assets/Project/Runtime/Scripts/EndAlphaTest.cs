using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAlphaTest : MonoBehaviour
{

   [SerializeField] GameObject endpanel;
    // Start is called before the first frame update
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            endpanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
