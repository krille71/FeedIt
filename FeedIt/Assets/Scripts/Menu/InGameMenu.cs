using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject InGameMenuUI;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject Censored;
    [SerializeField] private GameObject FinalScore;

    private ScoreCounter scoreCounter;

    void Start()
    {
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }

    void Update()
    {
        // Pause and resume by pressing escape if not in game over
        if (!GameOverMenu.active && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                PauseMenu.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        InGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenu.SetActive(false);
    }

    private void Pause()
    {
        InGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Censored.SetActive(true);
        InGameMenuUI.SetActive(true);
        FinalScore.GetComponent<TextMeshProUGUI>().text = "FINAL SCORE: " + scoreCounter.GetScore().ToString();
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
