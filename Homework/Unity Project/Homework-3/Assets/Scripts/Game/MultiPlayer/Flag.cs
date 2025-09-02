using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flag : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer && !IsHost) return;

        if (IsClient && collision.CompareTag("Player"))
        {
            string playerName = collision.GetComponent<SetPlayerName>().GetPlayerName();
            MultiplayerGameManager.Instance.DeclareWinnerServerRpc(playerName);
        }
    }
}
