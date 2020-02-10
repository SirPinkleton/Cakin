using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsUI;

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Continue()
    {
        Debug.Log("Need to save currently played level to a file, read in here to load it again? Open a new panel showing all of the levels to play?");
    }

    public void Options()
    {
        gameObject.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
