using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;

    [Header("Confirm/Retake")] 
    [SerializeField] private GameObject confirmUI;
    [SerializeField] private KeyCode confirmKey = KeyCode.Y;
    [SerializeField] private KeyCode retakeKey = KeyCode.N;
    [SerializeField] private FirstPersonLook firstPersonLook;

    [Header("Flash effect")] 
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")] 
    [SerializeField] private Animator fadingAnimation;

    [Header("Camera Mode")] 
    [SerializeField] private FPSCamSwitch camSwitcher;

    [Header("Moodboard")] 
    [SerializeField] private Moodboard moodboard;
    
    private Texture2D screenCapture;
    private bool viewingPhoto;
    public bool IsViewingPhoto => viewingPhoto;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        if (confirmUI != null)
        {
            confirmUI.SetActive(false);
        }
        
        UpdateCameraUI();
    }

    private void Update()
    {
        UpdateCameraUI();

        if (viewingPhoto)
        {
            if (Input.GetKeyDown(confirmKey))
            {
                ConfirmPhoto();
            }
            else if (Input.GetKeyDown(retakeKey))
            {
                RetakePhoto();
            }

            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.R) && camSwitcher.IsFirstPerson)
            StartCoroutine(CapturePhoto());
    }
    

    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        
        viewingPhoto = true;
        camSwitcher.ToggleLocked = true;
        
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        if (firstPersonLook != null)
        {
            firstPersonLook.SetCursorLocked(false);
        }
        
        ShowPhoto();
        
    }

    private void ShowPhoto()
    {
        //do flash 
        StartCoroutine(CameraFlashEffect());
        
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        
        photoFrame.SetActive(true);

        if (confirmUI != null)
        {
            confirmUI.SetActive(true);
        }
        
        fadingAnimation.Play("CamFadeAnimation");
    }

    IEnumerator CameraFlashEffect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }
    
    public void RetakePhoto()
    {
        ClosePreview(true);
    }

    public void ConfirmPhoto()
    {
        if (moodboard != null)
        {
            Texture2D saved = new Texture2D(screenCapture.width, screenCapture.height, TextureFormat.RGB24, false);
            saved.SetPixels(screenCapture.GetPixels());
            saved.Apply();

            moodboard.PlacePhoto(saved);
        }
        
        ClosePreview(false);
        camSwitcher.SetFirstPerson(false);
    }

    private void ClosePreview(bool relockCursor)
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);

        if (confirmUI != null)
        {
            confirmUI.SetActive(false);
        }
        
        if (relockCursor && firstPersonLook != null)
        {
            firstPersonLook.SetCursorLocked(true);
        }

        camSwitcher.ToggleLocked = false;
    }

    private void UpdateCameraUI()
    {
        bool shouldShow = camSwitcher.IsFirstPerson && !viewingPhoto;

        if (cameraUI.activeSelf != shouldShow)
        {
            cameraUI.SetActive(shouldShow);
        }
    }
}
