using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectFallController : MonoBehaviour
{
    [Header("Prefabs")] 
    [SerializeField] private GameObject goodFallingObject;
    [SerializeField] private GameObject badFallingObject;
    [SerializeField] private GameObject stunFallingObject;
	[SerializeField] private GameObject rainbowFallingObject;


    [Header("Timings")] 
    [SerializeField] private float startMinWait = 1.2f;
    [SerializeField] private float startMaxWait = 2.2f;
    [SerializeField] private float endMinWait = 0.35f;
    [SerializeField] private float endMaxWait = 0.8f;
    [SerializeField] private float rampDuration = 90f;

    [Header("Spawn Placements")] 
    [SerializeField] private float spawnY = 7f;
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;
    [SerializeField] private float minSeperation = 2f;

    [Header("Odds")] 
    [SerializeField, Range(0f, 1f)] private float badChance = 0.3f;
    [SerializeField, Range(0f, 1f)] private float stunChance = 0.1f;
	[SerializeField, Range(0f, 1f)] private float rainbowChance = 0.1f;


    private float lastX = float.NegativeInfinity;
    private float elapsed;

    private void Start() => StartCoroutine(SpawnLoop());

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float t = Mathf.Clamp01(elapsed / rampDuration);
            float wait = Random.Range(Mathf.Lerp(startMinWait, endMinWait, t), Mathf.Lerp(startMaxWait, endMaxWait, t));

            yield return new WaitForSeconds(wait);
            elapsed += wait;

            GameObject prefab = PickPrefab();
            Instantiate(prefab, new Vector3(PickX(), spawnY, 0f), Quaternion.identity);
        }
    }

    private GameObject PickPrefab()
    {
        float roll = Random.value;

        if (roll < stunChance)
            return stunFallingObject;
        if (roll < stunChance + badChance)
            return badFallingObject;
		if (roll < stunChance + badChance + rainbowChance)
			return rainbowFallingObject;
        return goodFallingObject;
    }

    private float PickX()
    {
        float x = Random.Range(minX, maxX);
        for (int i = 0; i < 5 && Mathf.Abs(x - lastX) < minSeperation; i++)
            x = Random.Range(minX, maxX);
        lastX = x;
        return x;
    }
}
