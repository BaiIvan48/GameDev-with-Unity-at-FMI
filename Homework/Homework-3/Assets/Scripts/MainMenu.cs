using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayIntro()
    {
        SceneManager.LoadScene("Game");
    }
    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerGame");
    }

    public void PlayRandomLevel()
    {
        SceneManager.LoadScene("PG Levels Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
