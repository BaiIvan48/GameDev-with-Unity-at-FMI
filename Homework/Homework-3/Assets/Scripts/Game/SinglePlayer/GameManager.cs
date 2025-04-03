using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameOverScreen gameOverScreen;

    public LevelCleared levelCleared;

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

