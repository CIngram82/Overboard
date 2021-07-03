using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFeedback : MonoBehaviour
{

  // [SerializeField] Material original;
   //[SerializeField]  Material highlighted;
   // Renderer rend;

    private void Start()
    {
      //  original = gameObject.GetComponent<Material>();
      //  rend = gameObject.GetComponent<Renderer>();
    }
    private void OnMouseOver()
    {
        if(!InspectObject.IsInspecting)
        {
            Debug.Log("Is hovering");
        }
    }

    private void OnMouseExit()
    {
        if(!InspectObject.IsInspecting)
        {
            Debug.Log("No longer hovering");
        }
    }
}
