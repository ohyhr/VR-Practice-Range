using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() // will load the correct scene to play game
    {
        SceneManager.LoadScene(0);
    }

    public void PlayTesting() // will load the correct scene to go into testing scene
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame() // will quit the game
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
