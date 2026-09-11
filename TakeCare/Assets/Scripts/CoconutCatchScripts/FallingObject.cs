using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Effects")] 
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private float stunDuration = 0f;
    
    [Header("Spawn Boundary")] 
    [SerializeField] private float killY = -10f;

    private bool collected;
    private bool IsHarmful => scoreValue < 0 || stunDuration > 0f;

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
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (IsHarmful && player != null && player.IsShieldActive())
            {
                Destroy(gameObject);
                return;
            }

            if (stunDuration > 0f && player != null)
            {
                player.Stun(stunDuration);
            }

            if (scoreValue != 0)
            {
                GameController.Instance.AddScore(scoreValue);
            }
            
            Destroy(gameObject);
        }
		else if (other.gameObject.CompareTag("Ground"))
        {
            collected = true;
            Destroy(gameObject);
        }
    }
}
