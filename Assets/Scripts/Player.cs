using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    [Tooltip("How fast player moves")]
    public float speed = 10;
    [Tooltip("How high player jumps")]
    public float jumpForce = 10f;
    [Tooltip("Mouse sensitivity")]
    public float sensitivity = 2;
    public Transform mainCam;
    public LayerMask groundLayer;
    public float gravity = -9.81f;

    float sideInput;
    float forwardInput;
    float yVelocity;
    float gravityAcceleration;
    float jumpSpeed;

    Vector2 appliedMouseDelta; // Where player is trying to face
    Vector2 currentMouseLook; // Where player is currently facing

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera look
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / sensitivity);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90); // Clamp look rotation to straight up and down

            mainCam.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right); // Rotate camera
            transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up); // Rotate player object to face camera direction
        }

        if (canMove)
        {
            // Jumping
            gravityAcceleration = gravity * Time.deltaTime; // Calculate gravity acceleration
            jumpSpeed = Mathf.Sqrt(jumpForce * -2 * gravity); // Calculate force at which player jumps, accounting for gravity
            if (IsGrounded() && Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpSpeed; // Allow player to jump if grounded
            }

            yVelocity += gravityAcceleration; // Add gravity force to player

            // Movement
            sideInput = Input.GetAxis("Vertical");
            forwardInput = Input.GetAxis("Horizontal");

            Quaternion yaw = Quaternion.Euler(0, mainCam.eulerAngles.y, 0); // Take into account where player is facing
            Vector3 movement = yaw * new Vector3(forwardInput * speed, 0, sideInput * speed);
            movement.y = yVelocity;
            movement *= Time.deltaTime;
            controller.Move(movement);
        }
    }

    bool IsGrounded() // Check if player is on ground
    {
        Vector3 start = controller.transform.TransformPoint(controller.center); // get center of player
        float rayLength = controller.height / 2 - controller.radius + 0.1f; // Distance from start to end of controller + bit extra to detect ground
        bool hasHit = Physics.SphereCast(start, controller.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
