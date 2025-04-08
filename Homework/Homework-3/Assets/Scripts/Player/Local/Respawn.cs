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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("FallZone"))
        {
            int health = playerHealth.getValue();
            playerHealth.setValue(health - 1);
            gameObject.SetActive(false);
            Invoke("GoToRespawnPoint", timeToRespawn);
        }
    }

    private void GoToRespawnPoint()
    {
        transform.position = respawnPoint.transform.position;
        gameObject.SetActive(true);
    }
}
