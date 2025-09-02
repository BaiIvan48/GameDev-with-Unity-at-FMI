using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerRespawn : NetworkBehaviour
{
    [SerializeField]
    private float respawnTime = 2f;

    private NetworkVariable<Vector3> respawnPosition = new NetworkVariable<Vector3>(
            Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    AudioManager audioManager;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            GameObject foundRespawn = GameObject.FindWithTag("Respawn");

            if (foundRespawn != null)
            {
                respawnPosition.Value = foundRespawn.transform.position;
            }
            else
            {
                Debug.LogError("Respawn point not found! Ensure there is a GameObject with tag 'Respawn'.");
                respawnPosition.Value = Vector3.zero;
            }

            transform.position = respawnPosition.Value;
        }
        if (IsClient)
        {
            transform.position = respawnPosition.Value;
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (IsOwner && col.gameObject.CompareTag("FallZone"))
        {
            gameObject.SetActive(false);
            RespawnServerRpc();

            Invoke("PlayRespawnSound", respawnTime / 2);
        }
    }

    private void PlayRespawnSound()
    {
        audioManager.PlaySFX(audioManager.respawn);
    }

    [ServerRpc]
    private void RespawnServerRpc()
    {
        RespawnClientRpc();
    }

    [ClientRpc]
    private void RespawnClientRpc()
    {
        Invoke(nameof(SmoothRespawnStep), respawnTime);
    }

    private void SmoothRespawnStep()
    {
        transform.position = respawnPosition.Value;
        gameObject.SetActive(true);
    }

}
