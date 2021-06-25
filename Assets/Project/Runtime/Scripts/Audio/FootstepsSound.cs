using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask metalLayer;
    public LayerMask woodLayer;
    public LayerMask carpetLayer;
    public LayerMask waterLayer;
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
            return;
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 4, carpetLayer))
        {
            audioScript.PlayCarpetFootsteps();
            return;
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 8, woodLayer))
        {
            audioScript.PlayWoodFootsteps();
            
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 8, waterLayer))
        {
            audioScript.PlayWaterFootsteps();
        }

    }


}

