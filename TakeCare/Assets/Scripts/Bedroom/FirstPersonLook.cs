using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private FPSCamSwitch camSwitcher;
    [SerializeField] private PhotoCapture photoCapture;

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

    [Header("Zoom")] 
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private float defaultFov = 60f;
    [SerializeField] private float minFov = 20f;
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float zoomSmoothing = 10f;
    
    
    private float yaw;
    private float pitch;
    private bool wasFirstPerson;
    private float targetFov;
    

    private void Awake()
    {
        if (camSwitcher == null)
            camSwitcher = GetComponent<FPSCamSwitch>();
    }


    void Update()
    {
        bool isFirstPerson = camSwitcher.IsFirstPerson;

        if (isFirstPerson && !wasFirstPerson)
        {
            yaw = 0f;
            pitch = 0f;
            targetFov = defaultFov;
            fpsCamera.m_Lens.FieldOfView = defaultFov;
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

        if (photoCapture != null && photoCapture.IsViewingPhoto)
            return;

        float zoomFactor = fpsCamera.m_Lens.FieldOfView / defaultFov;
        
        yaw += Input.GetAxis("Mouse X") * sensitivity * zoomFactor;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * zoomFactor;
        
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetFov = Mathf.Clamp(targetFov - scroll * zoomSpeed, minFov, defaultFov);

        fpsCamera.m_Lens.FieldOfView =
            Mathf.Lerp(fpsCamera.m_Lens.FieldOfView, targetFov, Time.deltaTime * zoomSmoothing);

    }

    public void SetCursorLocked(bool locked)
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
