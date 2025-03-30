using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        Debug.Log("Options Opened!");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
