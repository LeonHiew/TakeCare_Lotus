using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roomCamera;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;

    private bool isFirstPerson = false;

    public bool IsFirstPerson => isFirstPerson;

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
            roomCamera.Priority = 5;
            fpsCamera.Priority = 10;
        }
        else
        {
            roomCamera.Priority = 10;
            fpsCamera.Priority = 5;
        }
    }
}
