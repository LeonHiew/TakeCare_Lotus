using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuUI : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Slider volumeSlider;

    public string backButtonScene = "MainMenu";     
    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        volumeSlider.onValueChanged.RemoveListener(OnVolumeSliderChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnDropdownValueChanged(int value)
	{
        // Debug.Log("Selected Index: " + value);
        
		string selectedValue = dropdown.options[value].text;

		// Debug.Log("Selected: " + selectedValue);
        // Debug.Log(value);

        ChangeResolution(selectedValue);


	}
    
    public void OnVolumeSliderChanged(float value)
    {
        Debug.Log("Volume slider changed to: " + value);
        AudioListener.volume = Mathf.Clamp01(value);

    }

    public void ChangeResolution(string selectedValue)
    {

        if (selectedValue == "Max (4K)")
        {
            Screen.SetResolution(3840, 2160, true);
            Debug.Log("Resolution set to 4K");
        }
        else if (selectedValue == "High (1440p)")
        {
            Screen.SetResolution(2560, 1440, true);
            Debug.Log("Resolution set to 1440p");
        }
        else if (selectedValue == "Medium (1080p)")
        {
            Screen.SetResolution(1920, 1080, true);
            Debug.Log("Resolution set to 1080p");
        }
        else if (selectedValue == "Low (720p)")
        {
            Screen.SetResolution(1280, 720, true);
            Debug.Log("Resolution set to 720p");
        }
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        UnityEngine.SceneManagement.SceneManager.LoadScene(backButtonScene);
    }
}
