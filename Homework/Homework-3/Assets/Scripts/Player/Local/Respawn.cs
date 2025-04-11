using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject respawnPoint;

    [SerializeField]
    private float timeToRespawn = 1;

    private Health playerHealth;

    AudioManager audioManager;

    private void Start()
    {
        playerHealth = GetComponent<Health>();
        if (respawnPoint == null)
        {
            GameObject foundRespawn = GameObject.FindGameObjectWithTag("Respawn");
            if (foundRespawn != null)
            {
                respawnPoint = foundRespawn;
            }
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("FallZone"))
        {
            audioManager.PlaySFX(audioManager.hit);
            int health = playerHealth.getValue();
            playerHealth.setValue(health - 1);
            gameObject.SetActive(false);
            Invoke("PlayRespawnSound", timeToRespawn/2);
            Invoke("GoToRespawnPoint", timeToRespawn);
        }
    }

    private void PlayRespawnSound()
    {
            audioManager.PlaySFX(audioManager.respawn);
    }

    private void GoToRespawnPoint()
    {
        transform.position = respawnPoint.transform.position;
        gameObject.SetActive(true);
    }
}
