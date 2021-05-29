using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    float moveHorizontal;
    float moveVertical;

    [SerializeField] Animator camAnim;

    void Start()
    {
        speed = 5;
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
        camAnim.SetBool("isWalking", moveHorizontal != 0 || moveVertical != 0);
    }
}
