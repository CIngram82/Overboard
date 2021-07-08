using UnityEngine;
using System.Collections;

//Eric Edit
using InventorySystem.Collectable;

public class ItemInspector : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] Camera _inspectCamera;
    [Min(1)] [SerializeField] float _rotationSpeed = 1.0f;
    [Min(10)] [SerializeField] float _zoomSpeed = 10.0f;
    [SerializeField] float _distanceFromCamera = 1.5f;
    [SerializeField] float _radius = 1.0f;
    [Header("Object")]
    [SerializeField] Transform _parentTransform;
    [SerializeField] bool _isInspecting;

    //Eric Edits
    [SerializeField] LayerMask keysLayer;
    GameObject player;
    Ray _ray;

    Vector3 position;
    Vector3 objectCenter;
    Vector3 objectPos;
    Vector3 zoom;
    Vector3 forward;

    public void SetItemPosition(bool isInspecting)
    {
        _parentTransform = gameObject.transform.parent;
        forward = _inspectCamera.transform.forward * _distanceFromCamera;
        objectPos = _inspectCamera.transform.position + forward;
        _parentTransform.position = objectPos;
        _parentTransform.rotation = Quaternion.identity;
        _isInspecting = isInspecting;
    }

    void OnMouseScroll()
    {
        var zoomAmount = Input.mouseScrollDelta.y;
        zoom = _parentTransform.position;

        if (zoomAmount > 0.0f)
        {
            zoom += forward * (_zoomSpeed * Time.deltaTime);
        }
        else if (zoomAmount < 0.0f)
        {
            zoom -= forward * (_zoomSpeed * Time.deltaTime);
        }

        Vector3 offset = zoom - objectCenter;
        _parentTransform.position = objectCenter + Vector3.ClampMagnitude(offset, _radius);
    }
    public void OnMouseDown()
    {
        position = Input.mousePosition;

        //Eric Edits
        _ray = _inspectCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(_ray, out rayHit, 50.0f, keysLayer) && _isInspecting)
        {

            InspectForKey(rayHit.transform.gameObject);

            return;
        }
    }
    public void OnMouseDrag()
    {
        var deltaPosition = Input.mousePosition - position;

        var axis = Quaternion.AngleAxis(-90.0f, Vector3.forward) * deltaPosition;
        gameObject.transform.rotation = Quaternion.AngleAxis(deltaPosition.magnitude * _rotationSpeed, axis) * gameObject.transform.rotation;
        position = Input.mousePosition;
    }

    //Eric Edits
    void InspectForKey(GameObject objToInspect)
    {
        //print("Item clicked: " + objToInspect.name);
        if (objToInspect.name.Contains("face"))
        {
            //print("has \"face\"");
            if (objToInspect.name.Contains("b_"))        //f_ corrosponds to front animations and f_ to back animations
            {
                //print("has \"b_\"");
                objToInspect.GetComponentInParent<Animator>().SetBool("frontOpen", !objToInspect.GetComponentInParent<Animator>().GetBool("frontOpen"));      //toggle Value in animator of parent
                //print("Value changed");
            }
            else if (objToInspect.name.Contains("f_"))
            {
                //print("has \"f_\"");
                objToInspect.GetComponentInParent<Animator>().SetBool("backOpen", !objToInspect.GetComponentInParent<Animator>().GetBool("backOpen"));
                //print("Value changed");
            }
        }
        else if (objToInspect.name.Contains("cap"))
        {
            objToInspect.GetComponentInParent<Animator>().SetBool("lighterOpen", !objToInspect.GetComponentInParent<Animator>().GetBool("lighterOpen"));      //toggle Value in animator of parent

            if (objToInspect.GetComponentInParent<Animator>().GetBool("lighterOpen") == false) //if the lighter lid was just closed
            {
                objToInspect.GetComponentInParent<Animator>().SetBool("lighterFlick", false);
            }
        }
        else if (objToInspect.name.Contains("striker"))
        {
            objToInspect.GetComponentInParent<Animator>().SetBool("lighterFlick", true);
        }
        else if (objToInspect.name.Contains("key"))
        {
            objToInspect.GetComponentInParent<Animator>().SetBool("keyFound", true);      //set keyFound value in parent's animator to true
            StartCoroutine(waitAndPickUp(objToInspect));
        }
    }

    //Eric Added
    public IEnumerator waitAndPickUp(GameObject objToInspect)     //Allows FindKeyAnimation to play
    {
        print("wait");
        yield return new WaitForSeconds(2);
        print("pickup");
        objToInspect.GetComponentInParent<WorldItem>().PickUpItem(player);
    }

    void Update()
    {
        if (_isInspecting)
        {
            OnMouseScroll();
        }
    }
    void Start()
    {
        _parentTransform = gameObject.transform.parent;
        objectCenter = _parentTransform.position;

        //Eric Code
        player = Player.player.gameObject;
    }
    void Awake()
    {
        _isInspecting = false;
        _inspectCamera = _inspectCamera ? _inspectCamera : Player.inspectCam;
    }
}





