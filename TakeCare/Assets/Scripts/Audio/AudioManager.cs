using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip bedroomMusic;
    [SerializeField] private AudioClip scene2dMusic;
    [SerializeField] private AudioClip scene4Music;

    [Header("SFX")]

    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip highlightSFX;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private AudioClip selectSFX;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.loop = true;

        SceneManager.activeSceneChanged += OnSceneChanged;
    }


    private void Start()
    {
        ChangeSceneMusic(SceneManager.GetActiveScene());
    }


    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log("Scene changed from " + oldScene.name + " to " + newScene.name);

        ChangeSceneMusic(newScene);
    }


    private void ChangeSceneMusic(Scene scene)
    {
        Debug.Log("Changing music for scene: " + scene.name);

        if (scene.name == "MainMenu")
        {
            ChangeMusic(mainMenuMusic);
        }
        else if (scene.name == "Bedroom")
        {
            ChangeMusic(bedroomMusic);
        }
        else if (scene.name == "Scene2D")
        {
            ChangeMusic(scene2dMusic);
        }
        else if (scene.name == "Scene4")
        {
            ChangeMusic(scene4Music);
        }
        else
        {
            Debug.LogWarning("No music configured for scene: " + scene.name);
        }
    }


    private void ChangeMusic(AudioClip newMusic)
    {
        if (newMusic == null)
        {
            Debug.LogWarning("Music clip is null!");
            return;
        }

        if (musicSource.clip == newMusic && musicSource.isPlaying)
            return;

        Debug.Log("Changing music to: " + newMusic.name);

        musicSource.Stop();
        musicSource.clip = newMusic;
        musicSource.Play();
    }


    // SFX

    public void PlayHoverSFX()
    {
        sfxSource.PlayOneShot(hoverSFX);
    }

    public void PlayHighlightSFX()
    {
        sfxSource.PlayOneShot(highlightSFX);
    }

    public void PlayClickSFX()
    {
        sfxSource.PlayOneShot(clickSFX);
    }

    public void PlaySelectSFX()
    {
        sfxSource.PlayOneShot(selectSFX);
    }


    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}