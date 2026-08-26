using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] private Transform topPivot;
    [SerializeField] private Transform bottomPivot;

    [SerializeField] private Transform fish;
    
    //fish bar movement
    [SerializeField] private float timerMultiplicator = 3f;
    [SerializeField] private float smoothMotion = 1f;
    private float fishPosition;
    private float fishDestination;
    private float fishTimer;
    private float fishSpeed;
    
    //hook bar movement and size
    [SerializeField] private Transform hook;
    [SerializeField] private float hookSize = 0.1f;
    [SerializeField] private float hookPower = 0.5f;
    [SerializeField] private float hookPullPower = 0.01f;
    [SerializeField] private float hookGravityPower = 0.005f;
    [SerializeField] private float hookProgressDegrationPower = 0.1f;
    [SerializeField] private SpriteRenderer hookSpriteRenderer;
    private float hookPosition;
    private float hookProgress;
    private float hookPullVelocity;
    
    //progress bar container 
    [SerializeField] private Transform progressBarContainer;
    
    //win lose cons 
    [SerializeField] private float failTimer = 10f;
    private bool pause = false;
    


    private void Start()
    {
        Resize();
    }

    private void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;
    }


    private void Update()
    {
        if (pause)
            return;
        
        Fish();
        Hook();
        ProgressCheck();
    }

    private void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegrationPower * Time.deltaTime;

            failTimer -= Time.deltaTime;
            if (failTimer < 0f)
            { 
                Lose();
            }
        }

        if (hookProgress >= 1f)
        {
            Win();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    private void Lose()
    {
        pause = true;
        Debug.Log("You Lose!");
    }

    private void Win()
    {
        pause = true;
        Debug.Log("You Win!");
    }

    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        else
        {
            hookPullVelocity -= hookGravityPower * Time.deltaTime;
        }

        hookPosition += hookPullVelocity;
        
        if (hookPosition - hookSize / 2 <= 0f && hookPullVelocity < 0f)
        { 
            hookPullVelocity = 0f;
        }
        
        if (hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 0f)
        { 
            hookPullVelocity = 0f;
        }
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
           
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }

    void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }
}
