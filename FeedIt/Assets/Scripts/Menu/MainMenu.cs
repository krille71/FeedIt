using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject tMenu;
    [SerializeField] private GameObject creditMenu;
    [SerializeField] private GameObject controlMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToCreditsMenu()
    {
        tMenu.SetActive(false);
        creditMenu.SetActive(true);
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
