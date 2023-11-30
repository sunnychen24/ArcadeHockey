using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int p1Score = 0;
    private int p2Score = 0;
    public GameObject player1;
    public GameObject player2;
    private GameObject puck;
    private float timer = 180;
    private GameObject gameUI;
    private GameObject menuUI;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI scoreText;
    private bool gameOver = false;
    private bool isPaused;
    private bool overtime = false;
    private AudioSource audioSource;
    public AudioClip whistle;
    public AudioClip goalHorn;

    // Start is called before the first frame update
    void Start()
    {
        puck = GameObject.Find("Puck");
        gameUI = GameObject.Find("Game Canvas");
        menuUI = GameObject.Find("Menu Canvas");
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        timer = 60 * PlayerPrefs.GetInt("0");

        scoreText.text = p1Score.ToString() + ":" + p2Score.ToString();
        PauseGame(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (isPaused)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 0;

                if (p1Score == p2Score)
                {
                    overtime = true;
                }
            }
        }

        if (!overtime)
        {
            TimeSpan t = TimeSpan.FromSeconds(timer);
            string timeStr = "";

            if (t.Minutes > 0)
            {
                timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            }
            else
            {
                timeStr = string.Format("{0:D2}.{1:D2}", t.Seconds, t.Milliseconds);
            }

            timerText.text = timeStr;
        }
        else
        {
            timerText.text = "Overtime";
        }

        if (timer <= 0 && !gameOver && !overtime)
        {
            Time.timeScale = 0;
            gameOver = true;

            if (p1Score > p2Score)
            {
                menuUI.transform.Find("Vertical Group/Menu Text").GetComponent<TextMeshProUGUI>().text = "Player 1 Wins!";
            }
            else
            {
                menuUI.transform.Find("Vertical Group/Menu Text").GetComponent<TextMeshProUGUI>().text = "Player 2 Wins!";
            }

            menuUI.transform.Find("Vertical Group/Final Score").GetComponent<TextMeshProUGUI>().text = p1Score.ToString() + ":" + p2Score.ToString();

            gameUI.SetActive(false);
            menuUI.transform.Find("Vertical Group/Resume Button").gameObject.SetActive(false);
            menuUI.transform.Find("Vertical Group/Final Score").gameObject.SetActive(true);
            menuUI.SetActive(true);
        }
    }

    public void PauseGame(bool paused)
    {
        if (paused)
        {
            menuUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(false);
        }

        isPaused = paused;
        Time.timeScale = paused ? 0 : 1;
    }

    public IEnumerator GoalScored(int player)
    {
        Time.timeScale = 0;

        audioSource.PlayOneShot(goalHorn, 0.5f);
        yield return new WaitForSecondsRealtime(6);

        if (player == 0)
        {
            p1Score++;
        }
        else
        {
            p2Score++;
        }

        if (overtime)
        {
            overtime = false;
        }
        else
        {
            puck.GetComponent<FixedJoint2D>().connectedBody = null;

            puck.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            player1.GetComponent<Animator>().Play("Idle Right");
            player2.GetComponent<Animator>().Play("Idle Left");
            puck.GetComponent<Animator>().Play("Puck Idle");

            player1.transform.position = new Vector3(-7, 0, 0);
            player2.transform.position = new Vector3(7, 0, 0);

            if (player == 0)
            {
                puck.transform.position = new Vector3(5, 0, 0);
            }
            else
            {
                puck.transform.position = new Vector3(-5, 0, 0);
            }

            scoreText.text = p1Score.ToString() + ":" + p2Score.ToString();

            player1.GetComponent<PlayerController>().haspuck = false;

            if (player2.name.Equals("AI"))
            {
                player2.GetComponent<AiController>().ResetState();
            }

            audioSource.PlayOneShot(whistle, 0.5f);
            yield return new WaitForSecondsRealtime(2);

            Time.timeScale = 1;
        }
        
        yield return null;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
