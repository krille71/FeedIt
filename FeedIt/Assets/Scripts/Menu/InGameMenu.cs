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
    [SerializeField] private GameObject GameOverText;

    private ScoreCounter scoreCounter;

    void Start()
    {
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }

    void Update()
    {
        if (!GameOverMenu.active)
        {
            if (LevelGenerator.gameFinished)
            {
                GameOver();
            }

            // Pause and resume by pressing escape if not in game over
            else if (Input.GetKeyDown(KeyCode.Escape))
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
        LevelGenerator.gameFinished = false;
        Beast.isSleeping = false;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Resume();
        LevelGenerator.gameFinished = false;
        Beast.isSleeping = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        InGameMenuUI.SetActive(true);
        GameOverMenu.SetActive(true);

        FinalScore.GetComponent<TextMeshProUGUI>().text = "FINAL SCORE: " + scoreCounter.GetScore().ToString();

        if (!LevelGenerator.gameFinished)
        {
            Censored.SetActive(true);
            GameOverText.GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        }
        else
        {
            GameOverText.GetComponent<TextMeshProUGUI>().text = "DEMO FINISHED";
        }

        Time.timeScale = 0f;
    }
}
