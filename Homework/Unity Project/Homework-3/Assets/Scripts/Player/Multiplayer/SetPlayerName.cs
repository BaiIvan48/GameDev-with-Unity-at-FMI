using TMPro;
using Unity.Netcode;
using UnityEngine;
using Unity.Collections;

public class SetPlayerName : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerName;

    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Player 0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private NetworkVariable<Color> networkPlayerColor = new NetworkVariable<Color>(
           Color.white, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            networkPlayerName.Value = "Player " + NetworkManager.Singleton.ConnectedClients.Count;
            networkPlayerColor.Value = GetRandomColor();
        }

        ApplyNameAndColor();
        networkPlayerName.OnValueChanged += (_, _) => ApplyNameAndColor();
        networkPlayerColor.OnValueChanged += (_, _) => ApplyNameAndColor();
    }

    private void ApplyNameAndColor()
    {
        playerName.text = networkPlayerName.Value.ToString();
        playerName.color = networkPlayerColor.Value;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public string GetPlayerName()
    {
        return networkPlayerName.Value.ToString();
    }
}
