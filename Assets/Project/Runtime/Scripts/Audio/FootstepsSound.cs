using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask woodLayer;
    public LayerMask waterLayer;


    public static bool isInWater;
    public void CheckFloor()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 9, waterLayer))
        {
            AudioScript._instance.PlayWaterFootsteps();
            isInWater = true;
            Debug.Log("Stepping");
        }
       else if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 9, woodLayer))
       {
            AudioScript._instance.PlayWoodFootsteps();
            isInWater = false;
       }
        else
        {
            AudioScript._instance.PlayWaterFootsteps();

        }
   
    }
}

