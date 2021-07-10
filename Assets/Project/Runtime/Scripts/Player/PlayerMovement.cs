using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static bool canMove = true;

    [SerializeField] float speedMax;
    [SerializeField] Animator camAnim;
    [SerializeField] CameraController camControl;
    float startY;
    float speed;
    float moveHorizontal;
    float moveVertical;
    Vector3 movement;
    Rigidbody rig;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
        speedMax = 400;
        speed = speedMax;
        startY = gameObject.transform.position.y;
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

        CollisionErrorCheck(); // Forces the player back on the starting Y position values incase of collision error causing sinking
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;
        rig.velocity = movement * speed * Time.deltaTime;
        transform.rotation = camControl.RotatePlayer();
        if (canMove && !InspectObject.IsInspecting)
        {
            camAnim.SetBool("isWalking", moveHorizontal != 0 || moveVertical != 0);
        }
        else
        {
            camAnim.SetBool("isWalking", false);
        }
        if(FootstepsSound.isInWater)
        {
            speedMax = 250;
        }
        else
        {
            speedMax = 300;
        }
        if (PauseController.IsPaused)
        {
            camAnim.SetBool("isWalking", false);
        }

    }

    void CollisionErrorCheck()
    {
        if (gameObject.transform.position.y > startY + .2f || gameObject.transform.position.y < startY - .2f)
        {
            Debug.Log("Player Y Position Error. Reseting Y Position");
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, startY, gameObject.transform.position.z);
        }
    }

}
