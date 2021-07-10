using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask woodLayer;
    public LayerMask waterLayer;


    public static bool isInWater;
    public void CheckFloor()
    {

        if (Physics.Raycast(transform.position, Vector3.down, 8, waterLayer))
        {
            AudioScript._instance.PlayWaterFootsteps();
            isInWater = true;
        }
       else if (Physics.Raycast(transform.position, Vector3.down, 8, woodLayer))
       {
            AudioScript._instance.PlayWoodFootsteps();
            isInWater = false;
       }
   
    }
}

