using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask metalLayer;
    public LayerMask woodLayer;
    public LayerMask carpetLayer;
    public LayerMask waterLayer;


    public void CheckFloor()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 4, metalLayer))
        {
            AudioScript._instance.PlayMetalFootsteps();
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 4, carpetLayer))
        {
            AudioScript._instance.PlayCarpetFootsteps();
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 4, woodLayer))
        {
            AudioScript._instance.PlayWoodFootsteps();           
        }
        //else if (Physics.Raycast(transform.position, Vector3.down, 8, waterLayer))
        //{
        //    AudioScript._instance.PlayWaterFootsteps();
        //}
    }
}

