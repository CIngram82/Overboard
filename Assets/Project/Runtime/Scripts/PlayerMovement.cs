using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static bool canMove;
    [SerializeField] float speedMax;
    float speed;
    float moveHorizontal;
    float moveVertical;
    Vector3 movement;
    Rigidbody rig;
    [SerializeField] Animator camAnim;
    [SerializeField] CameraController camControl;

    
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        speedMax = 400;
        speed = speedMax;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            speed = speedMax;
        }
        else
        {
            speed = 0;
        }
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");

            movement = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;
            rig.velocity = movement * speed * Time.deltaTime;
            camControl.Rotate();
            camAnim.SetBool("isWalking", moveHorizontal != 0 || moveVertical != 0);
        
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            speedMax = 200;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            speedMax = 300;
        }
    }

}
