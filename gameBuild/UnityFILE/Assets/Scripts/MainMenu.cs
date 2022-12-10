using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
    SceneManager.LoadScene("Game");
    Time.timeScale = 1f;
    }

    public void QuitGame ()
    {
        
        Application.Quit();

    }

    public void loadSettings()
    {
        SceneManager.LoadScene("settings");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadControls()
    {
        SceneManager.LoadScene("controls");
    }
}
