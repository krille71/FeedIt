using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    [SerializeField] private GameObject InGameMenuUI;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject Censored;
    [SerializeField] private GameObject FinalScore;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject BackBoard;

    private LevelGenerator levelGenerator;
    private Beast beast;
    private ScoreCounter scoreCounter;

    void Start()
    {
        beast = GameObject.FindGameObjectWithTag("Beast").GetComponent<Beast>();
        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
    }

    void Update()
    {
        if(!GameIsPaused)
            Time.timeScale = 1f;

        if (!GameOverMenu.active)
        {
            if (levelGenerator.gameFinished)
            {
                GameOver();
            }

            // Pause and resume by pressing escape if not in game over
            else if (Input.GetKeyDown(KeyCode.P))
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
        GameOverMenu.SetActive(false);
        BackBoard.SetActive(false);
    }

    private void Pause()
    {
        InGameMenuUI.SetActive(true);
        BackBoard.SetActive(true);
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
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /*
        Initialize game over screen. Different text depending on if the beast is sleeping or is fighting the player 
    */
    public void GameOver()
    {
        GameIsPaused = true;
        InGameMenuUI.SetActive(true);
        GameOverMenu.SetActive(true);
        BackBoard.SetActive(true);

        FinalScore.GetComponent<TextMeshProUGUI>().text = "FINAL SCORE: " + scoreCounter.GetScore().ToString();

        if (!levelGenerator.gameFinished)
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
