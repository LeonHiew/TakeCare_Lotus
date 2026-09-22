using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private CamSwitcher camSwitcher;

    [Header("Look Settings")] 
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    [Header("cursor")] 
    [SerializeField] private bool lockCursorInFirstPerson = true;

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
        }
        else if (!isFirstPerson && wasFirstPerson)
        {
            transform.localRotation = Quaternion.identity;
            SetCursorLocked(false);
        }

        wasFirstPerson = isFirstPerson;

        if (!isFirstPerson)
            return;

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
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
}
