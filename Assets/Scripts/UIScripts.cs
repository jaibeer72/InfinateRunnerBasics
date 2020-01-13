using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScripts : MonoBehaviour
{
   public void QuitGame()
    {
        Application.Quit(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScean");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TryAgain()
    {
        ScoreScript.Score = 1;
        SceneManager.LoadScene("GameScean");
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
