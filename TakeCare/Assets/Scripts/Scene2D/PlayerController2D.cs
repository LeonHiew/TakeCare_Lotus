using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    float speedX, speedY;
    Rigidbody2D rb;

    public bool canMove = true;

    public float clickMoveDuration = 2f;
    public bool clickMoving = false;   

    bool onLand = false;

    int landEnterCount = 0;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // void OnApplicationFocus(bool hasFocus)
    // {
    //     if (hasFocus)
    //     {
    //         Cursor.visible = true;
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !clickMoving)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = transform.position.z;

            StartCoroutine(MoveToPoint(targetPos));
        }

        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

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

    public void EnablePlayerMovement()
    {
       canMove = true;
    }

    public void DisablePlayerMovement()
    {
        canMove = false;
    }

    private IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        clickMoving = true;
        DisablePlayerMovement();

        rb.velocity = Vector2.zero;

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < clickMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / clickMoveDuration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;

        clickMoving = false;
        EnablePlayerMovement();
    }
    
    
}
