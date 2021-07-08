using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    Animator anim;
    private Camera cam;
    private GameObject player;

    public float reach = 10f; // How far can the player reach
    private string animBoolName = "isOpen";
    [SerializeField] private bool playerEntered;
    [SerializeField] private bool showMessage = true;
    [SerializeField] string message;
    [SerializeField] public TextMeshProUGUI messageGUI;

    private int rayLayerMask;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        Debug.Assert(cam != null, "Main camera not found");
        // get the layer for the interact and invert the masking
        LayerMask iRayLM = LayerMask.NameToLayer("Interact");
        rayLayerMask = 1 << iRayLM.value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = true;
            if (showMessage)
            {
                messageGUI.text = message;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerEntered = false;
            messageGUI.text = "";
            showMessage = false;
        }
    }
    void Update()
    {
        if (playerEntered)
        {
            Vector3 rayStart = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if(Physics.Raycast(rayStart, cam.transform.forward, out hit, reach, rayLayerMask))
            {
                InteractableObject interactObj = isEqualToParent(hit.collider);
                // is the player looking at this object or a child of it?

                if(interactObj == null)
                {
                    // Not looking at us
                    return;
                }
                else
                {
                    Debug.Log(interactObj.gameObject.name);
                    showMessage = true;
                    bool isOpen = anim.GetBool(animBoolName);
                    if(Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
                    {
                        anim.enabled = true;
                        anim.SetBool(animBoolName, !isOpen);

                    }

                }

            }
            else
            {
                showMessage = false;
            }
        }
    }

    // look up the family tree of the object hit and see if one of them has the InteractableObject component
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
