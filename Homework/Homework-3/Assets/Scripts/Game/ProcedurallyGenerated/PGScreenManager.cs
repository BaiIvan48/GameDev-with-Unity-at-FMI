using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PGScreenManager : MonoBehaviour
{
    public static PGScreenManager Instance;

    public PGGameOver gameOverScreen;
    public PGWinScreen winScreen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GameOver(int keysCollected)
    {
        gameOverScreen.SetUp(keysCollected);
    }

    public void WinGame()
    {
        winScreen.SetUp();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
