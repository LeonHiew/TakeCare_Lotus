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
        
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
        UnityEngine.SceneManagement.SceneManager.LoadScene(settingsButtonScene);
    }

    
}
