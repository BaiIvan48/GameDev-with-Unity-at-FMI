using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] 
    private float powerUpDuration = 5f;
    private SpriteRenderer spriteRenderer;
    private bool isPoweredUp = false;
    private Color originalColor;

    public bool IsPoweredUp => isPoweredUp;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ActivatePowerUp()
    {
        if (!isPoweredUp)
        {
            StartCoroutine(PowerUpRoutine());
        }
    }

    private IEnumerator PowerUpRoutine()
    {
        isPoweredUp = true;
        float elapsed = 0f;

        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.SetMusicPitch(2f);

        while (elapsed < powerUpDuration)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
            elapsed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.color = originalColor;
        isPoweredUp = false;

        audio.SetMusicPitch(1f);
    }
}
