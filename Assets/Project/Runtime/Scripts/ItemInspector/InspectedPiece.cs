 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectedPiece : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask checkedLayer;
    [SerializeField] GameObject keyPiece;
    [SerializeField] float offset;
 

    private void Update()
    {
        RaycastHit rayHit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float dist = Vector3.Distance(Input.mousePosition, gameObject.GetComponentInParent<Transform>().position);

        if (Physics.Raycast(ray, out rayHit, dist + offset, checkedLayer) && Input.GetMouseButtonDown(0)) // either inspector layer or key layer should be used for this.
        {

            if (rayHit.collider.gameObject == gameObject)
            {
         
                if (gameObject == keyPiece)
                {
                    Debug.Log("Hit");
                    GrabObject.collectablesList.Add(gameObject);
                    keyPiece.SetActive(false);
                }
                else
                {
                    keyPiece.SetActive(true);
                    gameObject.SetActive(false);
                }
             
            }

        }
    }

    

}

