using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Highscores : MonoBehaviour
{
    public GameObject highscorePanel;
    public Canvas canvas;

    public void Start()
    {
        highscoreButton.onClick.AddListener(OnHighscoreButtonClick);
        highscorePanel.SetActive(false);
    }

    [Serializable]
    public class HighscoreEntry
    {
        public string PlayerName;
        public float[] RecordTime;

        public HighscoreEntry(string playerName, float[] recordTimes)
        {
            PlayerName = playerName;
            RecordTime = recordTimes;
        }
    }

    public List<HighscoreEntry> highscoreEntryList;

    public List<HighscoreEntry> GetHighscoreEntryList()
    {
        List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*_SavedData.json");

        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            HighscoreEntry highscoreEntry;
            highscoreEntry = JsonUtility.FromJson<HighscoreEntry>(json);
            string recordTimesStr = highscoreEntry.RecordTime != null
              ? String.Join(", ", highscoreEntry.RecordTime)
              : "null";
            Debug.Log("Loaded PlayerName: " + highscoreEntry.PlayerName + " - RecordTime: " + recordTimesStr);
            highscoreEntryList.Add(highscoreEntry);
        }

        return highscoreEntryList;
    }

    public TextMeshProUGUI highscoreText;

    public void DisplayHighscores()
    {
        highscoreText.text = "";  // Clear existing text
        highscoreEntryList = GetHighscoreEntryList();  // Get the updated list of highscores

        string highscoreList = "Player Name:\tLevel1(s):\tLevel2(s):\tLevel3(s):\n";
        string throwaway = "";
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            highscoreList += highscoreEntry.PlayerName;
            if (highscoreEntry.RecordTime != null)
            {
                int counter = 0;

                foreach (float recordTime in highscoreEntry.RecordTime)
                {
                    if (counter == 0)
                        throwaway += "\t\t\t" + recordTime.ToString("F2");  // Format the time to two decimal places
                    else if (counter == 1)
                    {
                        highscoreList += "\t\t\t" + recordTime.ToString("F2");  // Format the time to two decimal places

                    }
                    else
                    {
                        highscoreList += "\t\t" + recordTime.ToString("F2");  // Format the time to two decimal places
                    }
                    counter++;
                }
            }
            else
            {
                highscoreList += "\tNo record times";
            }
            highscoreList += "\n";  // Add a new line after each highscore entry
        }

        highscoreText.text = highscoreList;  // Update the TextMeshProUGUI component with the formatted string
    }



    public UnityEngine.UI.Button highscoreButton;
    //get public panel
    //on button click enable panel and display highscores
    public void OnHighscoreButtonClick()
    {
        highscorePanel.SetActive(true);
        if (canvas.GetComponent<Canvas>().enabled == false)
        {
            canvas.GetComponent<Canvas>().enabled = true;
        }
        DisplayHighscores();
    }
}