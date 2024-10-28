using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI instructionsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameOverCompletedText;
    public GameObject gameOverBackground;
    public GameObject gameCompletedBackground;
    public GameObject instructionsBackground;
    public Button startButton;
    public Button restartButton;
    public static bool isGameActive;
    public static bool isPaused = false;
    private int score;
    private float timer = 30f;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        gameOverCompletedText.gameObject.SetActive(false);
        
        gameOverBackground.SetActive(false);
        
        gameCompletedBackground.SetActive(false);
        
        restartButton.gameObject.SetActive(false);
        
        countdownText.gameObject.SetActive(false);
        
        instructionsText.gameObject.SetActive(true);
        
        restartButton.onClick.AddListener(RestartGame);
        
        startButton.onClick.AddListener(StartGameWithCountdown);

        isGameActive = false;

        ShowTitleScreen();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    void ShowTitleScreen()
    {
        titleScreen.SetActive(true);

        isGameActive = false;
        
        Time.timeScale = 0f;
    }

    void StartGameWithCountdown()
    {
        titleScreen.SetActive(false);
        
        instructionsBackground.SetActive(true);
        
        StartCoroutine(StartCountdown());
    }
    IEnumerator StartCountdown()
    {
        int countdownTime = 3;
        
        countdownText.gameObject.SetActive(true);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            
            yield return new WaitForSecondsRealtime(1f);
            
            countdownTime--;
        }

        countdownText.text = "Go Collect Apples!";
       
        yield return new WaitForSecondsRealtime(0.7f);

        countdownText.gameObject.SetActive(false);
        
        instructionsText.gameObject.SetActive(false);
        
        StartGame();
    }

    void StartGame()
    {
        instructionsText.gameObject.SetActive(false);
        
        instructionsBackground.SetActive(false);
        
        isGameActive = true;
        
        Time.timeScale = 1f;
        
        StartTimer();
    }

    void StartTimer()
    {  
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timer -= Time.unscaledDeltaTime;

        if (timer < 0)
        {
            timer = 0;

            Time.timeScale = 0f;

            Debug.Log("Oh no! Time ran out!   Score: " + score + " Try again!"); 
            
            GameOver();
        }

        int minutes = Mathf.FloorToInt(timer / 60);
        
        int seconds = Mathf.FloorToInt(timer % 60);
        
        timerText.text = "Timer - " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        scoreText.text = "Score - " + score;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        isGameActive = false;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        
        gameOverBackground.SetActive(true);
        
        isGameActive = false;
        
        restartButton.gameObject.SetActive(true);
    }

    public void ShowGameCompletedText()
    {
        gameOverCompletedText.gameObject.SetActive(true);
        
        gameCompletedBackground.SetActive(true);
        
        isGameActive = false;
        
        restartButton.gameObject.SetActive(true);
    }
}