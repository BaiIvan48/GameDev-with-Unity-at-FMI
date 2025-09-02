using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 12f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null && rb.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.trampoline);
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);
        }
    }
}
