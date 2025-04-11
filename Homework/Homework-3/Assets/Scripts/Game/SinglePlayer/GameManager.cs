using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameOverScreen gameOverScreen;

    public LevelCleared levelCleared;

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
        levelCleared.SetUp();
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

