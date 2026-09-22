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

    [Header("Flash effect")] 
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")] 
    [SerializeField] private Animator fadingAnimation;

    [Header("Camera Mode")] 
    [SerializeField] private CamSwitcher camSwitcher;
    
    private Texture2D screenCapture;
    private bool viewingPhoto;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        UpdateCameraUI();
    }

    private void Update()
    {
        UpdateCameraUI();
        
        if (!Input.GetKeyDown(KeyCode.R))
            return;

        if (viewingPhoto)
        {
            RemovePhoto();
        }
        else if (camSwitcher.IsFirstPerson)
        {
            StartCoroutine(CapturePhoto());
        }
    }
    

    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        
        viewingPhoto = true;
        
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        
    }

    private void ShowPhoto()
    {
        //do flash 
        StartCoroutine(CameraFlashEffect());
        
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        
        photoFrame.SetActive(true);
        
        fadingAnimation.Play("CamFadeAnimation");
    }

    IEnumerator CameraFlashEffect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }
    
    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
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
