using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float hookSize = 0.15f;
    [SerializeField] private float hookPower = 0.5f;
    [SerializeField] private float hookPullPower = 0.6f;
    [SerializeField] private float hookGravityPower = 0.9f;
    [SerializeField] private float hookProgressDegrationPower = 0.1f;
    [SerializeField] private float hookMaxSpeed = 1f;
    [SerializeField] private SpriteRenderer hookSpriteRenderer;
    private float hookPosition;
    private float hookProgress;
    private float hookPullVelocity;
    
    //progress bar container 
    [SerializeField] private Transform progressBarContainer;
    
    //fail timer and respawn delay 
    [SerializeField] private float failTimeLimit = 8f;
    [SerializeField] private float respawnDelay = 2f;
    private float failTimer;
    private float respawnTimer;


    private void Start()
    {
        Resize();
        ResetRound();
    }

    private void ResetRound()
    {
        hookProgress = 0f;
        hookPosition = hookSize / 2f;
        hookPullVelocity = 0f;
        failTimer = failTimeLimit;
        respawnTimer = 0f;
        
        SpawnFish();
        fish.gameObject.SetActive(true);

        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);

        Vector3 ls = progressBarContainer.localScale;
        ls.y = 0f;
        progressBarContainer.localScale = ls;
    }

    private void SpawnFish()
    {
        fishPosition = UnityEngine.Random.Range(0.15f, 0.85f);
        fishDestination = UnityEngine.Random.value;
        fishTimer = UnityEngine.Random.value * timerMultiplicator;
        fishSpeed = 0f;

        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
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
        if (FishGameController.Instance != null && FishGameController.Instance.IsGameOver)
            return;

        if (respawnTimer > 0f)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer <= 0f)
                ResetRound();
            return;
        }
        
        Fish();
        Hook();
        ProgressCheck();
    }

    private void ProgressCheck()
    {
        failTimer -= Time.deltaTime;
        if (failTimer <= 0f)
        {
            FishEscaped();
            return;
        }
        
        if(FishGameController.Instance != null)
            FishGameController.Instance.SetFailTimerUI(failTimer);
        
        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegrationPower * Time.deltaTime;
        }

        if (hookProgress >= 1f)
        {
            Win();
            return;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
        
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;
    }

    private void Win()
    {
        if(FishGameController.Instance != null)
            FishGameController.Instance.AddScore(1);

        EndCatch();
    }

    private void EndCatch()
    {
        respawnTimer = respawnDelay;
        hookProgress = 0f;
        
        fish.gameObject.SetActive(false);
        
        if(FishGameController.Instance != null)
            FishGameController.Instance.HideFailTimerUI();
        
        Vector3 ls = progressBarContainer.localScale;
        ls.y = 0f;
        progressBarContainer.localScale = ls;
    }

    private void FishEscaped()
    {
        Debug.Log("The Fish Got Away!");
        EndCatch();
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

        hookPullVelocity = Mathf.Clamp(hookPullVelocity, -hookMaxSpeed, hookMaxSpeed);
        hookPosition += hookPullVelocity * Time.deltaTime;
        
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
