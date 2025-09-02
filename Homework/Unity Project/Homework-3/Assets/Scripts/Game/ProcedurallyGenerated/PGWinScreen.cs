using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PGWinScreen : MonoBehaviour
{
    public void SetUp()
    {
        gameObject.SetActive(true);
    }

    public void TryPGLevelButton(int level)
    {
        switch (level)
        {
            case 1: LevelDificulty.selectedLevelDificulty = 1; break;
            case 2: LevelDificulty.selectedLevelDificulty = 2; break;
            case 3: LevelDificulty.selectedLevelDificulty = 3; break;
            case 4: LevelDificulty.selectedLevelDificulty = 4; break;
            case 5: LevelDificulty.selectedLevelDificulty = 5; break;

            default: LevelDificulty.selectedLevelDificulty = 1; break;
        }
        SceneManager.LoadScene("PG Levels");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
