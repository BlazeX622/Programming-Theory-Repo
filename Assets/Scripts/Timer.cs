using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float timer = 0f;

    [SerializeField] private Checkpoint checkpoint;
    private bool gameOver;

    private void OnEnable()
    {
        checkpoint.onGameOver += StopTime;
    }

    private void OnDisable()
    {
        checkpoint.onGameOver -= StopTime;
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }
        // Increment the timer based on real-time passed since the last frame
        timer += Time.deltaTime;

        // Calculate minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

        // Display the timer in the format MM:SS:SSS
        string timerText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        // Print or display the timer text as needed
        textMeshPro.text = "Timer: " + timerText;
        //Debug.Log("Timer: " + timerText);
    }

    private void StopTime()
    {
        GameManager.instance.currentTime = timer;
        if (GameManager.instance.currentTime < GameManager.instance.bestTime)
        {
            GameManager.instance.bestTime = GameManager.instance.currentTime;
            GameManager.instance.SaveFile();
        }
        gameOver = true;
    }
}
