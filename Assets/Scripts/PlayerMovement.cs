using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    Rigidbody rig;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    public float moveHorizontal;
   public  float moveVertical;
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

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            camAnim.SetBool("isWalking", true);
        }
        else
        {
            camAnim.SetBool("isWalking", false);
        }

    }


}
