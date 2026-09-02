using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public CinemachineVirtualCamera virtualCamera;

    private Animator animator;
    private CharacterController characterController;

    public float BaseFov = 50f;
    private Vector3 gravityVelocity;

    public CinemachineImpulseSource sprintImpulse;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = BaseFov;
        }
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movementInput = new Vector2(inputX, inputY);
        bool hasMovementInput = movementInput.magnitude > 0.1f;

        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputY);
        animator.SetBool("IsMoving", hasMovementInput);

        if (hasMovementInput)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputY + camRight * inputX;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 10f
            );
        }
        if (characterController.isGrounded)
        {
            gravityVelocity.y = -2f;
        }
        else
        {
            gravityVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    void OnAnimatorMove()
    {
        Vector3 movement = animator.deltaPosition;
        movement.y = gravityVelocity.y * Time.deltaTime;

        characterController.Move(movement);
    }
}