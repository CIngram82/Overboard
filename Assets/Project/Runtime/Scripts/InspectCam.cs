using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectCam : MonoBehaviour
{
    [SerializeField] float maxDistance = 50.0f;
    Camera _rayCamera;
    ItemInspector currentInspector;


    #region Mono
    void Update()
    {
        if (InspectObject.IsInspecting)
        {
            Ray _ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(_ray, out rayHit, maxDistance))
            {
                if (rayHit.collider.gameObject.TryGetComponent(out ItemInspector inspector))
                {
                    if (currentInspector != inspector)
                    {
                        currentInspector = inspector;
                    }
                }
            }
            if (!currentInspector) return;
            if (Input.GetMouseButtonDown(0))
                currentInspector.OnMouseDown();
            if (Input.GetMouseButton(0))
                currentInspector.OnMouseDrag();
        }
    }

    void Start()
    {
        _rayCamera = GetComponent<Camera>();
    }
    #endregion
}





