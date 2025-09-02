using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PGScreenManager : MonoBehaviour
{
    public static PGScreenManager Instance;

    public PGGameOver gameOverScreen;
    public PGWinScreen winScreen;

    AudioManager audioManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void GameOver(int keysCollected)
    {
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.lose);
        gameOverScreen.SetUp(keysCollected);
    }

    public void WinGame()
    {
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.win);
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
