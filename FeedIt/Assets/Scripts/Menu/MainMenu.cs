using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject tMenu;
    [SerializeField] private GameObject creditMenu;
    [SerializeField] private GameObject controlMenu;

    [SerializeField] private GameObject backBoard;
    [SerializeField] private GameObject logo;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToCreditsMenu()
    {
        tMenu.SetActive(false);
        creditMenu.SetActive(true);
        backBoard.SetActive(false);
        logo.SetActive(false);
    }

    public void SwitchToControlsMenu()
    {
        tMenu.SetActive(false);
        controlMenu.SetActive(true);
    }

    public void SwitchToTitleMenu()
    {
        tMenu.SetActive(true);
        creditMenu.SetActive(false);
        controlMenu.SetActive(false);
        backBoard.SetActive(true);
        logo.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
