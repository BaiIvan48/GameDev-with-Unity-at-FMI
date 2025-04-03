using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class MultiplayerGameManager : NetworkBehaviour
{
    private NetworkVariable<FixedString128Bytes> winnerName = new NetworkVariable<FixedString128Bytes>("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private bool gameFinished = false;

    public static MultiplayerGameManager Instance;

    public MultiplayerClientUI clientUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void RegisterClientUI(MultiplayerClientUI ui)
    {
        clientUI = ui;
        Debug.Log("Client UI registered in GameManager");
    }

    [ServerRpc(RequireOwnership = false)]
    public void DeclareWinnerServerRpc(string playerName)
    {
        if (gameFinished) return;

        gameFinished = true;
        winnerName.Value = playerName;

        Debug.Log($"Winner is {playerName}!");
        ShareWinnerClientRpc(playerName);
    }

    [ClientRpc]
    public void ShareWinnerClientRpc(string playerName)
    {
        MultiplayerClientUI[] allUIs = FindObjectsOfType<MultiplayerClientUI>();
        foreach (var ui in allUIs)
        {
            ui.ShowWinner(playerName);
        }
    }
}
