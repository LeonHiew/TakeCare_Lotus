using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Score Value")] 
    [SerializeField] private int scoreValue = 1;
    
    [Header("Spawn Boundary")] 
    [SerializeField] private float killY = -10f;

    private bool collected;

    private void Update()
    {
        if(transform.position.y < killY)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(collected)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {

            collected = true;
            GameController.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
		else if (other.gameObject.CompareTag("Ground"))
        {
            collected = true;
            Destroy(gameObject);
        }
    }
}
