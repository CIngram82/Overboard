using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float moveHorizontal;
    float moveVertical;
    Vector3 movement;
    Rigidbody rig;
    [SerializeField] Animator camAnim;
    [SerializeField] CameraController camControl;

    
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        
        movement = (moveHorizontal * transform.right +  moveVertical * transform.forward).normalized;
        rig.velocity = movement * speed * Time.deltaTime;
        camControl.Rotate();
        camAnim.SetBool("isWalking", moveHorizontal != 0 || moveVertical != 0);
    }

}
