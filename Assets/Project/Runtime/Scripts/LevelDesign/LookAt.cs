using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform target;
    Transform rope;
    [SerializeField] float globalScale;
    float scale;

    private void Start()
    {
        rope = gameObject.GetComponentInChildren<Transform>();
        scale = globalScale * transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.localScale = new Vector3(1f, 1f, Vector3.Distance(target.position, transform.position) * scale);
    }
}
