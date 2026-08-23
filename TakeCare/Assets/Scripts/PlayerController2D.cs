using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    float speedX, speedY;
    Rigidbody2D rb;

    bool onLand = false;

    int landEnterCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(speedX, speedY).normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Land"))
        {
            // Debug.Log("Player on Land");
            landEnterCount++;

            IsOnLand();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Land"))
        {
            // Debug.Log("Player left Land");
            landEnterCount--;

            IsOnLand();
        }
    }

    private void IsOnLand()
    {
        if (landEnterCount > 0)
        {
            onLand = true;
        }
        else
        {
            onLand = false;
            transform.position = new Vector3(0, 0, 0);
        }
    }

    
}
