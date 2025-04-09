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
        LevelDificulty.selectedLevelDificulty = 1;
        SceneManager.LoadScene("PG Levels");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
