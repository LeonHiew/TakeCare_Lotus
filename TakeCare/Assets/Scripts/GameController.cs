using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Canvas gameOverCanvas;
 
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private ObjectFallController objectFallController;
	[SerializeField] private GameObject retryButton; 
    
    [SerializeField] private GameObject homeButton; 
    
    private int score = 0;
    private float gameDuration = 10f;
    private float timeRemaining;
    private bool isGameOver = false;

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
        timeRemaining = gameDuration;
        UpdateScoreUI();
        
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

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

        if (timeRemaining <= 0f)
        {
            EndGame();
        }
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

    private void EndGame()
    {
        isGameOver = true;

        if (objectFallController != null)
            objectFallController.CancelInvoke("Fall");

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over! Final Score: " + score;
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
