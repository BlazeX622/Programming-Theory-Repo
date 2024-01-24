using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestTime;

    [SerializeField] private ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        GameManager.instance.carColor = color;
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        // Load the color AFTER AWAKE in MainManager which was loaded from Json
        ColorPicker.SelectColor(GameManager.instance.carColor);

        string bestTimerText = CalculateTime(GameManager.instance.bestTime);
        bestTime.text = "Best Time: " + bestTimerText;
    }    

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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

    // FOR THE BuTTONS
    public void SaveColorClicked()
    {
        GameManager.instance.SaveFile();
    }

    public void LoadColorClicked()
    {
        GameManager.instance.LoadFile();
        ColorPicker.SelectColor(GameManager.instance.carColor);
    }

    private string CalculateTime(float x)
    {
        // Calculate minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(x / 60f);
        int seconds = Mathf.FloorToInt(x % 60f);
        int milliseconds = Mathf.FloorToInt((x * 1000) % 1000);

        // Display the timer in the format MM:SS:SSS
        string timerText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timerText;
    }
}
