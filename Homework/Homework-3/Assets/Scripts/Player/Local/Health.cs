using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : Stats<int>
{
    void Awake()
    {
        DisplayIconCount[] displays = FindObjectsOfType<DisplayIconCount>();
        foreach (var display in displays)
        {
            if (display.GetStatType() == Stat.Health)
            {
                setValue(display.GetIconCount());
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            setValue(getValue() - 1);
        }
    }
    private void Update()
    {
        if (getValue() <= 0)
        {
            Pickup pickup = FindObjectOfType<Pickup>();
            int keysCollected = (pickup != null) ? pickup.getValue() : 0;
            gameObject.SetActive(false);

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene=="Game")
            {
                GameManager.Instance.GameOver(keysCollected);
            }
            else
            {
                PGScreenManager.Instance.GameOver(keysCollected);
            }
        }
    }
}
