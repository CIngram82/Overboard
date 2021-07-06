using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspector : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] Camera InspectCamera;
    [Min(1)] [SerializeField] float rotationSpeed = 1.0f;
    [Min(10)] [SerializeField] float zoomSpeed = 10.0f;
    [SerializeField] float radius = 1.0f;
    [Header("Object")]
    [SerializeField] Transform _parentTransform;
    [SerializeField] bool _isInspecting;

    Vector3 position;
    Vector3 objectCenter;
    Vector3 objectPos;
    Vector3 zoom;
    Vector3 forward;

    public void SetItemPosition(bool isInspecting)
    {
        _parentTransform = gameObject.transform.parent;
        forward = InspectCamera.transform.forward * 1.5f;
        objectPos = InspectCamera.transform.position + forward;
        _parentTransform.position = objectPos;
        _isInspecting = isInspecting;
    }

    void OnMouseScroll()
    {
        var zoomAmount = Input.mouseScrollDelta.y;
        zoom = _parentTransform.position;

        if (zoomAmount > 0.0f)
        {
            zoom += forward * (zoomSpeed * Time.deltaTime);
        }
        else if (zoomAmount < 0.0f)
        {
            zoom -= forward * (zoomSpeed * Time.deltaTime);
        }

        Vector3 offset = zoom - objectCenter;
        _parentTransform.position = objectCenter + Vector3.ClampMagnitude(offset, radius);
    }
    public void OnMouseDown()
    {
        position = Input.mousePosition;
    }
    public void OnMouseDrag()
    {
        var deltaPosition = Input.mousePosition - position;

        var axis = Quaternion.AngleAxis(-90.0f, Vector3.forward) * deltaPosition;
        gameObject.transform.rotation = Quaternion.AngleAxis(deltaPosition.magnitude * rotationSpeed, axis) * gameObject.transform.rotation;
        position = Input.mousePosition;
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
    }
    void Awake()
    {
        _isInspecting = false;
        InspectCamera = InspectCamera ? InspectCamera : Player.inspectCam;
    }
}





