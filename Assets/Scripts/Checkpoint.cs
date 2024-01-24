using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager checkPointManager;

    public event Action onGameOver;

    private void Awake()
    {
        checkPointManager = GameObject.Find("CheckPointManager").GetComponent<CheckPointManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return; 
        if (other.gameObject.CompareTag("Player"))
        {
            checkPointManager.numberOfCheckPointsPassed += 1;
            Debug.Log("CHECKPOINT");
        }

        if (checkPointManager.numberOfCheckPointsPassed >= checkPointManager.totalOfCheckPoints)
        {
            onGameOver?.Invoke();
        }
    }
}
