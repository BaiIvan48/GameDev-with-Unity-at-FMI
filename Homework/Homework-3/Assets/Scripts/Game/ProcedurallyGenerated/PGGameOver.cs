using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PGGameOver : MonoBehaviour
{
    public TMP_Text collectedKeysText;

    public void SetUp(int keys)
    {
        gameObject.SetActive(true);
        if (keys != 1)
        {
            collectedKeysText.text = keys.ToString() + " Keys collected";
        }
        else
        {
            collectedKeysText.text = "1 Key collected";
        }
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
