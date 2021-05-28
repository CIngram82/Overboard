using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask groundLayer;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    // Why have the 2 below as public they are not used in any other script.
    public float moveHorizontal;
    public float moveVertical;

    // What are the below 3 used for?
    // If not needed please remove them. 
    Rigidbody rig;
    float yMovement;
    bool canJump;

    [SerializeField] Animator camAnim;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        canJump = true;
        speed = 5;
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
        /*
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            camAnim.SetBool("isWalking", true);
        }
        else
        {
            camAnim.SetBool("isWalking", false);
        }
        */
        // This can be cleaned up into 1 line of code.

        camAnim.SetBool("isWalking", moveHorizontal != 0 || moveVertical != 0);
    }
}
