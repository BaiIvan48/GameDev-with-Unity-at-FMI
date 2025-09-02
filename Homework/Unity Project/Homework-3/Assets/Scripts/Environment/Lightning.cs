using UnityEngine;

public class Lightning : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            audioManager.PlaySFX(audioManager.lightning_pickup);
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp != null)
            {
                powerUp.ActivatePowerUp();
            }
            Destroy(gameObject);
        }
    }
}
