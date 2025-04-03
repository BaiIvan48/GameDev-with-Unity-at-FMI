using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
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

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
