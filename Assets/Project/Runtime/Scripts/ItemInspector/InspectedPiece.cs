using UnityEngine;
using Inventory.Collectable;

public class InspectedPiece : MonoBehaviour
{
    static Camera cam;

    [SerializeField] LayerMask checkedLayer;
    [SerializeField] GameObject keyPiece;
    [SerializeField] float offset;
    Transform parent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float dist = Vector3.Distance(Input.mousePosition, parent.position);

            if (Physics.Raycast(ray, out RaycastHit rayHit, dist + offset, checkedLayer)) // either inspector layer or key layer should be used for this.
            {
                if (rayHit.collider.gameObject == gameObject)
                {
                    if (gameObject == keyPiece)
                    {
                        Debug.Log("Hit");
                        rayHit.transform.gameObject.GetComponentInParent<WorldItem>().PickUpItem(gameObject);
                        // remove item from inventory
                        
                        //GrabObject.collectablesList.Add(gameObject);
                        //keyPiece.SetActive(false);
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
    void Start()
    {
        cam = Player.Camera;
        parent = gameObject.GetComponentInParent<Transform>();
    }
}
