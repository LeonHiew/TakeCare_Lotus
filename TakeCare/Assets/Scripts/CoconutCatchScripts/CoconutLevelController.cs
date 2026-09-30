using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoconutLevelController : MonoBehaviour
{
    [Header("Load to main")] 
    [SerializeField] private string homeSceneName = "SampleScene";
    [SerializeField] private string lvl1SceneName = "SampleScene";
    [SerializeField] private string lvl2SceneName = "SampleScene";
    [SerializeField] private string lvl3SceneName = "SampleScene";
    
    
    [Header("UI Buttons")] 
    [SerializeField] private GameObject lvl1Button; 
    [SerializeField] private GameObject lvl2Button;
    [SerializeField] private GameObject lvl3Button;
    [SerializeField] private GameObject homeButton; 
    
    
    public void lvl1Clicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvl1SceneName);
    }
    
    public void lvl2Clicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvl2SceneName);
    }
    
    public void lvl3Clicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvl3SceneName);
    }
    public void HomeClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeSceneName);
    }
    
    
}
