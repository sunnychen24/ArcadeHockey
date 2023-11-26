using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject menu;
    private int minutes;
    private GameObject title;
    private GameObject playButton;
    private GameObject quitButton;
    private GameObject gameLength;
    private GameObject horizontalGroup;
    private GameObject minutesText;
    private GameObject confirmButton;
    private GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu");
        title = menu.transform.Find("Vertical Group/Title").gameObject;
        playButton = menu.transform.Find("Vertical Group/Play Button").gameObject;
        quitButton = menu.transform.Find("Vertical Group/Quit Button").gameObject;
        gameLength = menu.transform.Find("Vertical Group/Game Length").gameObject;
        horizontalGroup = menu.transform.Find("Vertical Group/Horizontal Group").gameObject;
        minutesText = menu.transform.Find("Vertical Group/Horizontal Group/Minutes").gameObject;
        confirmButton = menu.transform.Find("Vertical Group/Confirm Button").gameObject;
        backButton = menu.transform.Find("Vertical Group/Back Button").gameObject;

        minutes = PlayerPrefs.GetInt("0", 0);

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

        gameLength.SetActive(true);
        horizontalGroup.SetActive(true);
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

    public void Back()
    {
        gameLength.SetActive(false);
        horizontalGroup.SetActive(false);
        confirmButton.SetActive(false);
        backButton.SetActive(false);

        title.SetActive(true);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("0", minutes);
        SceneManager.LoadScene("GameControllerTest");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
