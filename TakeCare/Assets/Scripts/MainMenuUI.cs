using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
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
        SceneManager.LoadScene("GameScene");
    }

    public void onOptionsButtonClicked()
    {
        Debug.Log("Options button clicked");
        SceneManager.LoadScene("OptionsScene");
    }

    public void onQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
