using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject menu;
    private int minutes;
    private int aiLevel;
    private GameObject title;
    private GameObject playButton;
    private GameObject quitButton;
    private GameObject gameSettings;
    private GameObject horizontalGroup;
    private GameObject horizontalGroup2;
    private GameObject horizontalGroup3;
    private GameObject minutesText;
    private GameObject gameTypeText;
    private GameObject aiDifficultyText;
    private GameObject confirmButton;
    private GameObject backButton;
    private int gameType = 0;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu");
        title = menu.transform.Find("Vertical Group/Title").gameObject;
        playButton = menu.transform.Find("Vertical Group/Play Button").gameObject;
        quitButton = menu.transform.Find("Vertical Group/Quit Button").gameObject;
        gameSettings = menu.transform.Find("Vertical Group/Game Settings").gameObject;
        horizontalGroup = menu.transform.Find("Vertical Group/Horizontal Group").gameObject;
        horizontalGroup2 = menu.transform.Find("Vertical Group/Horizontal Group (1)").gameObject;
        horizontalGroup3 = menu.transform.Find("Vertical Group/Horizontal Group (2)").gameObject;
        minutesText = menu.transform.Find("Vertical Group/Horizontal Group/Minutes").gameObject;
        gameTypeText = menu.transform.Find("Vertical Group/Horizontal Group (1)/Game Type").gameObject;
        aiDifficultyText = menu.transform.Find("Vertical Group/Horizontal Group (2)/AI Difficulty").gameObject;
        confirmButton = menu.transform.Find("Vertical Group/Confirm Button").gameObject;
        backButton = menu.transform.Find("Vertical Group/Back Button").gameObject;

        minutes = PlayerPrefs.GetInt("0", 0);
        aiLevel = PlayerPrefs.GetInt("1", 0);

        if (minutes == 0)
        {
            minutes = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        title.SetActive(false);
        playButton.SetActive(false);
        quitButton.SetActive(false);

        if (minutes > 1)
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minutes";
        }
        else
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minute";
        }

        if (gameType % 2 == 0)
        {
            gameTypeText.GetComponent<TextMeshProUGUI>().text = "1 Player Game";

            if (aiLevel == 0)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
            }
            else if (aiLevel == 1)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Medium";
            }
            else if (aiLevel == 2)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
            }
            else
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Goon";
            }

            horizontalGroup3.SetActive(true);
        }
        else
        {
            gameTypeText.GetComponent<TextMeshProUGUI>().text = "2 Player Game";
        }

        gameSettings.SetActive(true);
        horizontalGroup.SetActive(true);
        horizontalGroup2.SetActive(true);
        confirmButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void AddTime()
    {
        if (minutes < 10)
        {
            minutes++;
        }

        if (minutes > 1)
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minutes";
        }
        else
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minute";
        }
    }

    public void RemoveTime()
    {
        if (minutes > 1)
        {
            minutes--;
        }

        if (minutes > 1)
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minutes";
        }
        else
        {
            minutesText.GetComponent<TextMeshProUGUI>().text = minutes.ToString() + " Minute";
        }
    }

    public void ChangeGameType()
    {
        gameType++;

        if (gameType % 2 == 0)
        {
            gameTypeText.GetComponent<TextMeshProUGUI>().text = "1 Player Game";

            if (aiLevel == 0)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
            }
            else if (aiLevel == 1)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Medium";
            }
            else if (aiLevel == 2)
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
            }
            else
            {
                aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Goon";
            }

            horizontalGroup3.SetActive(true);
        }
        else
        {
            gameTypeText.GetComponent<TextMeshProUGUI>().text = "2 Player Game";
            horizontalGroup3.SetActive(false);
        }
    }

    public void IncreaseDifficulty()
    {
        if (aiLevel < 3)
        {
            aiLevel++;
        }

        if (aiLevel == 0)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
        } else if (aiLevel   == 1)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Medium";
        } else if (aiLevel == 2)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
        } else
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Goon";
        }
    }

    public void DecreaseDifficulty()
    {
        if (aiLevel > 0)
        {
            aiLevel--;
        }

        if (aiLevel == 0)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Easy";
        }
        else if (aiLevel == 1)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Medium";
        }
        else if (aiLevel == 2)
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Hard";
        }
        else
        {
            aiDifficultyText.GetComponent<TextMeshProUGUI>().text = "Goon";
        }
    }

    public void Back()
    {
        gameSettings.SetActive(false);
        horizontalGroup.SetActive(false);
        horizontalGroup2.SetActive(false);
        horizontalGroup3.SetActive(false);
        confirmButton.SetActive(false);
        backButton.SetActive(false);

        title.SetActive(true);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("0", minutes);

        if (gameType % 2 == 0)
        {
            PlayerPrefs.SetInt("1", aiLevel);
            SceneManager.LoadScene("AiTest");
        }
        else
        {
            SceneManager.LoadScene("GameControllerTest");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
