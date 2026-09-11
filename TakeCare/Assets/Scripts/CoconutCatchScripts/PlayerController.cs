using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")] 
    [SerializeField] private float moveSpeed = 10f; //15 if accel, 10 if shift sprint
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Player acceleration")] 
    [SerializeField] private float acceleration = 45f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float turnAcceleration = 100f;
    [SerializeField] private float dragInfluence = 1.5f;
    
    
    [Header("Dash ability")] 
    [SerializeField] private bool dashEnabled = true;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    [SerializeField] private float dashSpeed = 30f; //25 if accel, 30 if shift sprint
    [SerializeField] private float dashDuration = 0.15f; //0.1 if accel, 0.15 if shift sprint
    [SerializeField] private float dashCooldown = 1f;
    
    [Header("Shield ability")] 
    [SerializeField] private bool shieldEnabled = true;
    [SerializeField] private KeyCode shieldKey = KeyCode.E;
    [SerializeField] private float shieldCooldown = 8f;
    [SerializeField] private float shieldDuration = 5f;
    [SerializeField] private GameObject shieldSprite;

    [Header("Player Stunned")] 
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Color stunColor = Color.yellow;
    
    
    
    private float horizontalInput;
    private Rigidbody2D rb;
    
    private bool isDashing =  false;
    private bool canDash = true;
    private float lastDashDirection = 1f;

    private bool isShieldActive = false;
    private bool canActivateShield = true;

    private float stunTimer;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (playerSprite == null)
            playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null)
            originalColor = playerSprite.color;

        if (shieldSprite != null)
            shieldSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            horizontalInput = 0f;

            if (stunTimer <= 0f)
            {
                stunTimer = 0f;
                if (playerSprite != null)
                {
                    playerSprite.color = originalColor;
                }
            }

            return;
        }
        
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
            lastDashDirection = horizontalInput;

        if (dashEnabled && canDash && !isDashing && Input.GetKeyDown(dashKey))
        {
            StartCoroutine(Dash());
        }
        
        if (shieldEnabled && canActivateShield && !isShieldActive && Input.GetKeyDown(shieldKey))
        {
            StartCoroutine(Shield());
        }
            
    }

    private void FixedUpdate()
    {
        if(isDashing)
            return;
        
        //for sprint movement (no acceleration, MAKE SURE TO COMMENT OUT ACCELERATION)
        
        /*float currentSpeed = Input.GetKey(sprintKey) ? moveSpeed * runMultiplier : moveSpeed;
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);*/

        //for player acceleration (no sprint, MAKE SURE TO COMMENT OUT SPRINT)
        
        float targetSpeed = horizontalInput * moveSpeed;
        float currentSpeed = rb.velocity.x;

        float rate;
        if (horizontalInput == 0)
        {
            float speedFactor = Mathf.Abs(currentSpeed) / Mathf.Max(moveSpeed, 0.01f);
            rate = deceleration * (1f + speedFactor * dragInfluence);
        }
        else if (currentSpeed != 0f && Mathf.Sign(horizontalInput) != Mathf.Sign(currentSpeed))
        {
            rate = turnAcceleration;
        }
        else
        {
            rate = acceleration;
        }

        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, rate * Time.fixedDeltaTime);
        rb.velocity = new Vector2(newSpeed, rb.velocity.y);

    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        
        float dashDirection = lastDashDirection;
        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);
        
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Shield()
    {
        isShieldActive = true;
        canActivateShield = false;
        
        if(shieldSprite != null)
            shieldSprite.SetActive(true);
        
        yield return new WaitForSeconds(shieldDuration);
        isShieldActive = false;
        
        if(shieldSprite != null)
            shieldSprite.SetActive(false);
        
        yield return new WaitForSeconds(shieldCooldown);
        canActivateShield = true;
    }
    
    //to enable/ disable dash ability from other scripts 
    public void SetDashEnabled(bool enabled)
    {
        dashEnabled = enabled;
    }
    
    //to enable/ disable shield ability from other scripts 
    public void SetShieldEnabled(bool enabled)
    {
        shieldEnabled = enabled;
    }
    
    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    public void Stun(float duration)
    {
        stunTimer = Mathf.Max(stunTimer, duration);

        if (playerSprite != null)
            playerSprite.color = stunColor;
    }

    public bool IsStunned()
    {
        return stunTimer > 0f;
    }
    
    
}


