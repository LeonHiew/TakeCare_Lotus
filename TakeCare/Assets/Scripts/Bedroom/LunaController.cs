using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LunaController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private Camera mainCamera;

    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float gravityValue = -9.81f;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 15.0f; // Speed of the turn toward the mouse
    public LayerMask groundMask;       // Layer assigned to your floor/ground

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Cache the main camera for performance
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        // Ground Check using CharacterController built-in flag
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; 
        }

        // Handle Mouse Aiming Rotation
        RotateTowardsMouse();

        // Read Keyboard/Gamepad Inputs (Legacy Input Manager)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the raw movement direction vector (World space)
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Apply Horizontal Movement if input exists
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Apply Constant Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Apply Final Vertical Movement (Gravity force)
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void RotateTowardsMouse()
    {
        // Cast a ray from the camera through the mouse screen position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits the ground layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            // Determine the direction from the character to the mouse hit point
            Vector3 lookDirection = hit.point - transform.position;
            
            // Keep the rotation strictly on the horizontal Y-axis (prevent tilting up/down)
            lookDirection.y = 0f; 

            if (lookDirection.magnitude > 0.1f)
            {
                // Smoothly rotate to look at the cursor position
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
