using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public bool aboveZero = true;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float z = Input.GetAxis("Vertical");    // W/S or Up/Down
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // keep grounded
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        //if(Position.y > 0) velocity.y += gravity * Time.deltaTime;
        //else velocity.y +=  Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

