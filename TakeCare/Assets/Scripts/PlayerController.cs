using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;

    private float moveSpeed = 15f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GoodItem"))
        {
            playerScore += 1;
        }
    }*/
}


