using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] string message;
    [SerializeField] float timeShowen = 3.0f;
    [SerializeField] string animBoolName = "isOpen";

    bool playerEntered = false;
    bool messageShown = false;
    Animator anim;
    GameObject player;
    Camera cam;
    float reach = 10f; // How far can the player reach
    int rayLayerMask;


    void Start()
    {
        anim = GetComponent<Animator>();
        player = Player.player.gameObject;
        // anim.enabled = false;
        anim.SetBool(animBoolName, false);

        cam = CameraController.Camera;
        //get the layer for the interact and invert the masking
        LayerMask iRayLM = LayerMask.NameToLayer("Interact");
        rayLayerMask = 1 << iRayLM.value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = true;
            if (playerEntered && !messageShown)
            {
                StartCoroutine(PromptDoorMessage());
                messageShown = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = false;
        }
    }
    void Update()
    {
        if (playerEntered && !PauseController.IsPaused && !FindObjectOfType<ClueInventoryUI>().isJournalOpen && !InspectObject.IsInspecting)
        {
            Vector3 rayStart = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(rayStart, cam.transform.forward, out hit, reach, rayLayerMask))
            {
                InteractableObject interactObj = isEqualToParent(hit.collider);
                 //is the player looking at this object or a child of it?
                 
                if (interactObj == null)
                {
                   // Not looking at us
                    return;
                }
                else
                {
                    bool isOpen = anim.GetBool(animBoolName);
                    if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
                    {
                        anim.enabled = true;
                        anim.SetBool(animBoolName, !isOpen);
                        AudioScript._instance.PlaySoundEffect("Door Creak");
                    }
                }
            }
        }
    }

    IEnumerator PromptDoorMessage()
    {
        Player.playerPromptText.text = message;
        yield return new WaitForSeconds(timeShowen);

        Player.playerPromptText.text = string.Empty;
        yield return null;
    }

    //look up the family tree of the object hit and see if one of them has the InteractableObject component
    private InteractableObject isEqualToParent(Collider other)
    {
        InteractableObject interactObj = null;
        try
        {
            int maxWalk = 5;
            interactObj = other.GetComponent<InteractableObject>();

            GameObject currentGO = other.gameObject;
            for (int i = 0; i < maxWalk; i++)
            {
                // is the ray cast hitting the object with this script. 
                if (currentGO.Equals(this.gameObject))
                {
                    interactObj = GetComponent<InteractableObject>();
                    if (interactObj == null) interactObj = currentGO.GetComponentInParent<InteractableObject>();
                    break;          //exit loop early.
                }
                //not equal to if reached this far in loop. move to parent if exists.
                if (currentGO.transform.parent != null)     //is there a parent
                {
                    currentGO = currentGO.transform.parent.gameObject;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return interactObj;
    }
}
