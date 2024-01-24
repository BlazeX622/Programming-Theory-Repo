using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public static GameManager instance;
    public Material material;

    // for SaveData
    public Color carColor; // new variable declared
    public float bestTime; // new variable declared
    public float currentTime;    

    private void Awake()
    {
        // start of new code
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadFile();
    }

    private void FixedUpdate()
    {
        material.color = carColor;
    }

    [System.Serializable]
    class SaveData
    {
        public Color carColor;
        public float timeTrial;
    }

    public void SaveFile()
    {
        SaveData data = new SaveData();
        data.carColor = carColor;
        data.timeTrial = bestTime;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            carColor = data.carColor;
            bestTime = data.timeTrial;
        }
    }
}
