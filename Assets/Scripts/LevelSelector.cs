using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public static int selectedLevel; // Public variable to store the selected level

    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;


    public static int SelectedlLevel()
    {
        return selectedLevel;
    }

    void Start()
    {
        // Add listeners to the buttons
        buttonLevel1.onClick.AddListener(() => SelectLevel(1));
        buttonLevel2.onClick.AddListener(() => SelectLevel(2));
        buttonLevel3.onClick.AddListener(() => SelectLevel(3));

        // Optionally initialize the buttons' states
        ResetButtonColors();
    }

    public void SelectLevel(int level)
    {
        selectedLevel = level;
        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        // Reset all buttons to default color
        ResetButtonColors();

        // Highlight the selected button
        switch (selectedLevel)
        {
            case 1:
                buttonLevel1.GetComponent<Image>().color = Color.white;
                break;
            case 2:
                buttonLevel2.GetComponent<Image>().color = Color.white;
                break;
            case 3:
                buttonLevel3.GetComponent<Image>().color = Color.white;
                break;
        }
    }

    private void ResetButtonColors()
    {
        buttonLevel1.GetComponent<Image>().color = Color.gray;
        buttonLevel2.GetComponent<Image>().color = Color.gray;
        buttonLevel3.GetComponent<Image>().color = Color.gray;
    }
}
