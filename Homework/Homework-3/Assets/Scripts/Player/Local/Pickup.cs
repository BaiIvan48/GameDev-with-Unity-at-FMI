using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : Stats<int>
{
    private void Awake()
    {
        setValue(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            setValue(getValue() + 1);
            Destroy(other.gameObject);

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Game" && getValue() == 5)
            {
                GameManager.Instance.WinGame();
            }
            else if (currentScene == "PG Levels" && getValue() == LevelDificulty.selectedLevelDificulty)
            {
                PGScreenManager.Instance.WinGame();
            }
        }
        
    }
}
