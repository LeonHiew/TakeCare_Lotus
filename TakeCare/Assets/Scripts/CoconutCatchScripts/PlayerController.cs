using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")] 
    [SerializeField] private float moveSpeed = 10f; 
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; 
    
    [Header("Dash ability")] 
    [SerializeField] private bool dashEnabled = true;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1.5f;
    
    [Header("Shield ability")] 
    [SerializeField] private bool shieldEnabled = true;
    [SerializeField] private KeyCode shieldKey = KeyCode.E;
    [SerializeField] private float shieldCooldown = 8f;
    [SerializeField] private float shieldDuration = 5f;
    [SerializeField] private GameObject shieldSprite;
    
    
    
    private float horizontalInput;
    private Rigidbody2D rb;
    
    private bool isDashing =  false;
    private bool canDash = true;
    private float lastDashDirection = 1f;

    private bool isShieldActive = false;
    private bool canActivateShield = true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (shieldSprite != null)
            shieldSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        
        float currentSpeed = Input.GetKey(sprintKey) ? moveSpeed * runMultiplier : moveSpeed;
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
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
    
    
}


