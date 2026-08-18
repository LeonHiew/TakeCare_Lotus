using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GoodItem"))
            {
                GameController.Instance.AddScore(1);
            }

            Destroy(this.gameObject);
        }
		
		else Destroy(this.gameObject);
    }
}
