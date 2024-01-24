using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public int numberOfCheckPointsPassed = -1;
    public int totalOfCheckPoints;
    private int startTotalOfCheckPoints;

    [SerializeField] private PauseMenu pauseMenu;

    private void OnEnable()
    {
        pauseMenu.onResetGame += ResetValues;
    }

    private void OnDisable()
    {
        pauseMenu.onResetGame -= ResetValues;
    }

    // Start is called before the first frame update
    void Start()
    {
        startTotalOfCheckPoints = totalOfCheckPoints;
    }

    private void ResetValues()
    {
        numberOfCheckPointsPassed = -1;
        totalOfCheckPoints = startTotalOfCheckPoints;
    }
}
