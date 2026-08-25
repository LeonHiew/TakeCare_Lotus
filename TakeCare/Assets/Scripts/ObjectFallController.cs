using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFallController : MonoBehaviour
{
    [SerializeField] private float minWait = 1f;
    [SerializeField] private float maxWait = 3f;


    public GameObject fallingObject;
    
    // Start is called before the first frame update
    void Start()
    {
        ScheduleNextFall();
    }

	void ScheduleNextFall()
	{
		float nextWait = Random.Range(minWait, maxWait);
		Invoke(nameof(Fall), nextWait);
	}

    void Fall()
    {
        Instantiate(fallingObject, new Vector3(Random.Range(-7, 8), 7, 0), Quaternion.identity);
		ScheduleNextFall();
    }

   
}
