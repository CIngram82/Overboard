using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public LayerMask metalLayer;
    public LayerMask woodLayer;
    public LayerMask carpetLayer;
    public LayerMask waterLayer;
    int theHitLayer;
   [SerializeField] LayerMask materialsLayer;

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
   


       // RaycastHit hit;
       //if (Physics.Raycast(transform.position, Vector3.down,out hit,8, materialsLayer))
       //{
       //     theHitLayer = hit.transform.gameObject.layer;
       //    // Debug.Log(hit.transform.name);
       //     if(waterLayer == (waterLayer|1<<theHitLayer))
       //     {
       //         AudioScript._instance.PlayWaterFootsteps();
       //          isInWater = true;
       //        // Debug.Log("Water");
       //     }
       //     else if(carpetLayer == (carpetLayer | 1 << theHitLayer))
       //     {
       //         AudioScript._instance.PlayCarpetFootsteps();
       //        // Debug.Log("Carpet");
       //         isInWater = false;
       //     }
       //     else if(woodLayer == (woodLayer | 1 << theHitLayer))
       //     {
       //         AudioScript._instance.PlayWoodFootsteps();
       //         isInWater = false;
       //     }
       //     else if(metalLayer == (metalLayer | 1 << theHitLayer))
       //     {
       //         AudioScript._instance.PlayMetalFootsteps();
       //         isInWater = false;
       //     }
       //}

    }
}

