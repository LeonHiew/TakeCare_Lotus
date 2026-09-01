using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class FishGameController : MonoBehaviour
{
    public static FishGameController Instance;
    
    [Header("Load to main")] 
    [SerializeField] private string homeSceneName = "SampleScene";
    
    [Header("UI Buttons")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
	[SerializeField] private GameObject retryButton; 
    [SerializeField] private GameObject homeButton;
    [SerializeField] private TextMeshProUGUI failTimerText;
    
    [Header("Game Duration")] 
    [SerializeField] private float gameDuration = 30f;

    [Header("Catch Results")] 
    [SerializeField] private TextMeshProUGUI catchResultText;
    
    private int score = 0;
    private float timeRemaining;
    private bool isGameOver = false;

    public bool IsGameOver => isGameOver;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        timeRemaining = gameDuration;
        score = 0;
        isGameOver = false;
        
        UpdateScoreUI();
        UpdateTimerUI();
        HideFailTimerUI();
        
        if(gameOverText != null)
            gameOverText.gameObject.SetActive(false);

		if (retryButton != null)
			retryButton.SetActive(false);
        
        if (homeButton != null)
            homeButton.SetActive(false);
    }
    

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerUI();
            EndGame();
            return;
        }
        
        UpdateTimerUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
    
    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    private void EndGame()
    {
        isGameOver = true;
        HideFailTimerUI();
        
        catchResultText.gameObject.SetActive(false);

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over! Fish Caught: " + score;
        }

		if (retryButton != null)
			retryButton.SetActive(true);
        
        if (homeButton != null)
            homeButton.SetActive(true);

		Time.timeScale = 0f;
    }

    public void SetFailTimerUI(float secondsRemaining)
    {
        if (failTimerText == null)
            return;
        
        if(!failTimerText.gameObject.activeSelf)
            failTimerText.gameObject.SetActive(true);

        failTimerText.text = "Catch in: " + Mathf.CeilToInt(secondsRemaining);
        failTimerText.color = secondsRemaining <= 3f ? Color.red : Color.white;
    }

    public void HideFailTimerUI()
    {
        if(failTimerText != null)
            failTimerText.gameObject.SetActive(false);
    }
   
	public void RetryClicked()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void HomeClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeSceneName);
    }

    public void ShowCatchResult(string message, Color color)
    {
        if (catchResultText == null)
            return;
        
        catchResultText.gameObject.SetActive(true);
        catchResultText.text = message;
        catchResultText.color = color;
    }

    public void HideCatchResult()
    {
        if(catchResultText != null)
            catchResultText.gameObject.SetActive(false);
    }
    
}
