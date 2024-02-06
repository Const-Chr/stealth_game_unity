using Lecture;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerData : GenericSingleton<PlayerData>
{
    [Serializable]
    public class Data
    {
        public string PlayerName;
        public float[] RecordTime;
        public Data(string Pname, int numberOfLevels)
        {
            PlayerName = Pname;
            RecordTime = new float[numberOfLevels];
             
        }

    }
    public Data data;
    public int _currentLevel;

    public override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        data.PlayerName = "Player";

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentLevel = scene.buildIndex;
        if (scene.name.Equals("CharacterSetup"))
            return;

        string filePath = Path.Combine(Application.persistentDataPath, $"{data.PlayerName}_SavedData.json");
        Debug.Log(data.PlayerName);
        if (File.Exists(filePath))
        {
            
            LoadData(filePath);
            Debug.Log(data.RecordTime[_currentLevel]);
            Debug.Log(data.PlayerName);
        }
        else
        {
            data = new Data(data.PlayerName, SceneManager.sceneCountInBuildSettings);
        }
    }

    public void UpdateTimeElapsedForLevel(float timeElapsed)
    {
        if (_currentLevel >= 0 && _currentLevel < data.RecordTime.Length)
        {
            if (timeElapsed < data.RecordTime[_currentLevel]  || data.RecordTime[_currentLevel] == 0)
            {
                data.RecordTime[_currentLevel] = timeElapsed;
                Debug.Log($"New Record Time for Level {_currentLevel}: {timeElapsed}");
                SaveData();
            }

        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);
        string filePath = Path.Combine(Application.persistentDataPath, $"{data.PlayerName}_SavedData.json");
        File.WriteAllText(filePath, json);
        Debug.Log(filePath + "\n");
    }

    public void LoadData(string fullPath)
    {
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<Data>(json);
        }
        data.PlayerName = Path.GetFileNameWithoutExtension(fullPath).Split('_')[0];
    }

    private void OnApplicationQuit()
    {
        //SaveData();
    }
}