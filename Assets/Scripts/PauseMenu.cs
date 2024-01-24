using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI currentTime;
    [SerializeField] private GameObject pauseMenu;

    //private CheckPointManager checkPointManager;
    [SerializeField] private Checkpoint checkpoint;

    public event Action onResetGame;

    private void OnEnable()
    {
        checkpoint.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        checkpoint.onGameOver -= GameOver;
    }

    private void Update()
    {
        string bestTimerText = CalculateTime(GameManager.instance.bestTime);
        string currentTimerText = CalculateTime(GameManager.instance.currentTime);

        bestTime.text = "Best Time: " + bestTimerText;
        currentTime.text = "Current Time: " + currentTimerText;
    }

    private string CalculateTime(float x)
    {
        // Calculate minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt( x / 60f);
        int seconds = Mathf.FloorToInt( x % 60f);
        int milliseconds = Mathf.FloorToInt((x * 1000) % 1000);

        // Display the timer in the format MM:SS:SSS
        string timerText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timerText;
    }

    public void GameOver()
    {
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        onResetGame?.Invoke();
        SceneManager.LoadScene(1);        
    }

    public void BackToMainMenu()
    {
        GameManager.instance.SaveFile();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        GameManager.instance.SaveFile();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
