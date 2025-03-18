using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject respawnPoint;

    [SerializeField]
    private float timeToRespawn = 1;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("FallZone"))
        {
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
