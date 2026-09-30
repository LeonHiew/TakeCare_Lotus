using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlayerController : MonoBehaviour
{
    private Animator animator;
    public bool animation_busy = false;
    public Quaternion desired_rotation;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float move_amount = Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.z));

        bool hasMovementInput = move_amount > 0.1f;
        //animator.SetBool("IsMoving", hasMovementInput);

        animator.SetFloat("MoveAmount", move_amount);
        // if (animation_busy) {return; }
        // if(move_amount > 0f)
        // {
        //     float turn_angle = Mathf.Abs(Vector3.SignedAngle(transform.forward, movement, Vector3.up));
        //     if (turn_angle >= 165f)
        //     {
        //         animator.CrossFade("walkingturn", .25f);
        //         // if (animator.GetBool("isMoving"))
        //         // {
        //         //     animator.CrossFade("walkingturn", .25f);
        //         // }
        //         // else
        //         // {
        //         //     animator.CrossFade("fullturn", .15f);
        //         // }
                
        //         desired_rotation = Quaternion.LookRotation(movement, Vector3.up);
        //     }
            Quaternion target_rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500 * Time.deltaTime);
        }
    }
