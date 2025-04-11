using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : Stats<int>
{
    AudioManager audioManager;
    private void Awake()
    {
        setValue(0);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            audioManager.PlaySFX(audioManager.key);
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
