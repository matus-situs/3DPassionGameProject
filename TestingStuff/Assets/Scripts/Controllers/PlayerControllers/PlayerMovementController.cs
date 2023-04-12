using UnityEngine;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float movementSpeed = 6f;
    [SerializeField]
    private float gravity = -14.715f;
    [SerializeField]
    private float jumpHeight = 10f;
    private Vector3 velocity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Animations animationsController;
    [SerializeField]
    private Animator animator;
    private bool isJumping = false, inAir = false;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        animationsController = new Animations(animator);
    }

    void Update()
    {
        playerMove();
        playerJump();
        playerGravity();
    }
    private void playerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && !isJumping)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (controller.isGrounded && !inAir)
            {
                animationsController.PlayAnimaton("Player_Walk");
            }

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
        else if (!isJumping && controller.isGrounded)
        {
            animationsController.PlayAnimaton("Player_Idle");
        }
        else if (!Physics.Raycast(this.transform.position, -transform.up, 10))
        {
            inAir = false;
        }
        else
        {
            inAir = true;
            animationsController.PlayAnimaton("Player_Falling");
        }

        Debug.Log("Grounded " + controller.isGrounded + " in air" + inAir + " ray " + Physics.Raycast(this.transform.position, -transform.up, 10));
    }
    private void playerJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            isJumping = true;
            animationsController.PlayAnimaton("Player_Jump");
        }
    }
    private void playerGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void playerGoUp()
    {
        isJumping = false;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        animationsController.PlayAnimaton("Player_Falling");
    }
    public void playerLanded()
    {
        animationsController.PlayAnimaton("Player_Idle");
    }
}
