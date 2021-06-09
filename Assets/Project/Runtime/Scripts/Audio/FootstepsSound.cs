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
            audioScript.PlayMetalFootsteps();
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 4, carpetLayer))
        {
            audioScript.PlayCarpetFootsteps();
        }
        else
        {
            audioScript.PlayWoodFootsteps();
        }
       
    }


}
