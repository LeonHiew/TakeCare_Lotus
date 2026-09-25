using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private CamSwitcher camSwitcher;

    [Header("Look Settings")] 
    [SerializeField] private float sensitivity = 1.2f;
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 45f;
    [SerializeField] private float minYaw = -45f;
    [SerializeField] private float maxYaw = 45f;

    [Header("cursor")] 
    [SerializeField] private bool lockCursorInFirstPerson = true;

    [Header("Movement lock")] 
    [SerializeField] private MonoBehaviour[] scriptsToDisableFP;
    
    private float yaw;
    private float pitch;
    private bool wasFirstPerson;

    private void Awake()
    {
        if (camSwitcher == null)
            camSwitcher = GetComponent<CamSwitcher>();
    }


    void Update()
    {
        bool isFirstPerson = camSwitcher.IsFirstPerson;

        if (isFirstPerson && !wasFirstPerson)
        {
            yaw = 0f;
            pitch = 0f;
            SetCursorLocked(true);
            SetMovementEnabled(false);
        }
        else if (!isFirstPerson && wasFirstPerson)
        {
            transform.localRotation = Quaternion.identity;
            SetCursorLocked(false);
            SetMovementEnabled(true);
        }

        wasFirstPerson = isFirstPerson;

        if (!isFirstPerson)
            return;

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

    }

    private void SetCursorLocked(bool locked)
    {
        if (!lockCursorInFirstPerson)
            return;

        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    private void SetMovementEnabled(bool enabled)
    {
        foreach (MonoBehaviour script in scriptsToDisableFP)
        {
            if (script != null)
            {
                script.enabled = enabled;
            }
        }
    }
}
