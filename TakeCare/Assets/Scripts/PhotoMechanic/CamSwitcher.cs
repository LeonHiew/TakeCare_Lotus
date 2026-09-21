using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook tpsCamera;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;

    private bool isFirstPerson = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCamera();
        }
    }

    private void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
        {
            tpsCamera.Priority = 5;
            fpsCamera.Priority = 10;
        }
        else
        {
            tpsCamera.Priority = 10;
            fpsCamera.Priority = 5;
        }
    }
}
