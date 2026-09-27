using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FPSCamSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roomCamera;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;

    private bool isFirstPerson = false;
    public bool ToggleLocked { get; set; }

    public bool IsFirstPerson => isFirstPerson;

    //check if player has pressed button toggle to swap between first person and third person camera 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !ToggleLocked)
        {
            ToggleCamera();
        }
    }

    //changes the priority of third person and first person cameras to swap/ transition cameras using cinemachine 
    private void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
        {
            roomCamera.Priority = 5;
            fpsCamera.Priority = 10;
        }
        else
        {
            roomCamera.Priority = 10;
            fpsCamera.Priority = 5;
        }
    }

    public void SetFirstPerson(bool value)
    {
        if (isFirstPerson == value)
            return;
        
        ToggleCamera();
    }
}
