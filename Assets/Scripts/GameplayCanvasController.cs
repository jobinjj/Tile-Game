using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayCanvasController : MonoBehaviour
{
    public Button exitButton;
    public Button restartButton;
    public TMP_Text timerText;
    public TMP_Text levelText;
    public int initialMinutes = 5;  // Set the initial minutes here
    private float currentTimeInSeconds;
    private int selectedLevel;
    public Animator animator;
    private bool IsGameOver;
    public Button gameOverHomeButton;
    public Button gameOverRetryButton;
    public Button gameWonHomeButton;
    public Button gameWonRetryButton;
    public Button gameWonNextLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        levelText.text = $"Level {selectedLevel}";
        gameOverHomeButton.onClick.AddListener(LoadHome);
        gameWonHomeButton.onClick.AddListener(LoadHome);
        exitButton.onClick.AddListener(LoadHome);
        restartButton.onClick.AddListener(Restart);   
        gameOverRetryButton.onClick.AddListener(Restart);   
        gameWonRetryButton.onClick.AddListener(Restart);   
        gameWonNextLevelButton.onClick.AddListener(NextLevel);   
    }

    private void NextLevel()
    {
        selectedLevel++;
        PlayerPrefs.SetInt("SelectedLevel", selectedLevel);
        if (selectedLevel > 3)
        {
            SceneManager.LoadScene("StartingScene");
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetTimerDetails(float timeInMinutes)
    {
        currentTimeInSeconds = timeInMinutes * 60;
        UpdateTimerText();
    }

    private void LoadHome()
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void ShowGameWon()
    {
        animator.SetTrigger("ShowGameWon");
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
        if(levelsUnlocked < 3)
        {
            levelsUnlocked++;
            PlayerPrefs.SetInt("LevelsUnlocked", levelsUnlocked);
            PlayerPrefs.SetInt("SelectedLevel", selectedLevel);
        }
    
    }

    public void ShowGameOver()
    {
        animator.SetTrigger("ShowGameOver");
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver) return;
        if (currentTimeInSeconds > 0)
        {
            currentTimeInSeconds -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            // Timer reached zero, you can handle the timer expiration here
            Debug.Log("Timer Expired!");
            IsGameOver = true;
            currentTimeInSeconds = 0;
            UpdateTimerText();
            ShowGameOver();
            // Optionally, stop the timer or perform other actions
        }
    }
    void UpdateTimerText()
    {
        // Format the time as mm:ss
        int minutes = Mathf.FloorToInt(currentTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(currentTimeInSeconds % 60);

        // Update the UI text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
