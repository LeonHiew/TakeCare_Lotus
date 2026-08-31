using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class FishGameController : MonoBehaviour
{
    public static FishGameController Instance;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
	[SerializeField] private GameObject retryButton; 
    [SerializeField] private GameObject homeButton;

    [SerializeField] private float gameDuration = 30f;
    
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
   
	public void RetryClicked()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void HomeClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
