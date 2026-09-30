using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float armstretchtimer = 14f;
    public float crytimer = 10f;
    private Animator animator;
    private CharacterController characterController;

    private Vector3 gravityVelocity;
    private float armstretchtime;
    private float crytime;
    private bool bIsTired = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            crytime = 0f;
        }
        else
        {
            crytime += Time.deltaTime;
            if (crytime >= crytimer)
            {
                crytime = 0f;
                int layer = animator.GetLayerIndex("UpperBody");
                animator.CrossFadeInFixedTime("angry", 0.15f, layer);
            }
        }
        
        armstretchtime += Time.deltaTime;
        if (armstretchtime >= armstretchtimer)
        {
            armstretchtime = 0f;
            int layer = animator.GetLayerIndex("UpperBody");
            if (bIsTired)
            {
                animator.CrossFadeInFixedTime("yawn", 0.15f, layer);
            }
            else
            {
                animator.CrossFadeInFixedTime("stretch", 0.15f, layer);
            }
            bIsTired = !bIsTired;
        }

        animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), 1f);
        
        
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