using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene2DUI : MonoBehaviour
{
    // Start is called before the first frame update

    public string backButtonScene = "MainMenu";     
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        UnityEngine.SceneManagement.SceneManager.LoadScene(backButtonScene);
    }
}
