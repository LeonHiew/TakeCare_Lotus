using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update

    public string startButtonScene = "GameScene";     
    public string optionsButtonScene = "OptionsMenu";     
    // public string quitButtonScene = "QuitScene";     

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        SceneManager.LoadScene(startButtonScene);
    }

    public void onOptionsButtonClicked()
    {
        Debug.Log("Options button clicked");
        SceneManager.LoadScene(optionsButtonScene);
    }

    public void onQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
