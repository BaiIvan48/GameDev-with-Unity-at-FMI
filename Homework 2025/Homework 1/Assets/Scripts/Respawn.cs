using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] 
    private GameObject respawnPoint;
    [SerializeField] 
    private LayerMask respawnLayer;
    [SerializeField]
    private float timeToRespawn = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(((1 << col.gameObject.layer) & respawnLayer.value) != 0)
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
