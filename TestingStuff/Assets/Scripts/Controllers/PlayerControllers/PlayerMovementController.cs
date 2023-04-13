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
    [SerializeField]

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        playerMove();
        playerGravity();
    }
    private void playerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }
    public void playerJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void playerGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
