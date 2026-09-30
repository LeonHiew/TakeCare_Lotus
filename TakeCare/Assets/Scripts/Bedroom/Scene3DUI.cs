using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene3DUI : MonoBehaviour
{
    // Start is called before the first frame update

    public string settingsButtonScene = "MainMenu";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnSettingsButtonClicked();
        }
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");

        // Restore mouse before switching scenes
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(settingsButtonScene);
    }

    
}
