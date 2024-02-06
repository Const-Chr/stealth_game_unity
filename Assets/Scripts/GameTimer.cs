using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public  float timeElapsed;
    public float minutes, seconds;

    //function to get time elapsed
    public  float GetTimeElapsed()
    {
        return timeElapsed;
    }

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer Text not assigned!");
            return;
        }
        timeElapsed = 0;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        DisplayTime(timeElapsed);
    }

    void DisplayTime(float timeToDisplay)
    {
         minutes = Mathf.FloorToInt(timeToDisplay / 60);
         seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("    {0:00}:{1:00}", minutes, seconds);
    }
}
