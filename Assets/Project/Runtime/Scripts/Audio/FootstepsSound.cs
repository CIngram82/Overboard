using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask metalLayer;
    public LayerMask woodLayer;
    public LayerMask carpetLayer;
    AudioScript audioScript;

    void Start()
    {
        audioScript = FindObjectOfType<AudioScript>();
    }

    public void CheckFloor()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 4, metalLayer))
        {
            Debug.DrawRay(transform.position, Vector2.down * 4, Color.red, .1f);
            audioScript.PlayMetalFootsteps();
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 4, carpetLayer))
        {

            Debug.DrawRay(transform.position, Vector2.down * 4, Color.blue, .2f);
            audioScript.PlayCarpetFootsteps();
        }
        else
        {
            audioScript.PlayWoodFootsteps();
            Debug.DrawRay(transform.position, Vector2.down * 4, Color.blue, .2f);
        }
       
    }


}
