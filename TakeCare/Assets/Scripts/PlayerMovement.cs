using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public CinemachineCamera virtualCamera;

    private Animator animator;
    public float BaseFov = 50f;

    public CinemachineImpulseSource sprintImpulse;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (virtualCamera != null)
        {
            virtualCamera.Lens.FieldOfView = BaseFov;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 movementInput = new Vector2(inputX, inputY);
        bool hasMovementInput = movementInput.magnitude > 0.1f;

        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputY);
        animator.SetBool("IsMoving", hasMovementInput);
        animator.SetBool("IsSprinting", Input.GetKey(KeyCode.LeftShift) && hasMovementInput);

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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}