using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerClientUI : NetworkBehaviour
{
    [SerializeField]
    private Canvas menuCanvas;

    [SerializeField]
    private TextMeshProUGUI playerNameField;

    [SerializeField]
    private GameObject winnerScreen;

    void Start()
    {
        if (menuCanvas == null)
        {
            menuCanvas = GetComponent<Canvas>();
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsClient)
        {
            MultiplayerGameManager.Instance?.RegisterClientUI(this);
        }
    }

    public void ShowWinner(string winnerName)
    {
        winnerScreen.SetActive(true);
        playerNameField.text = winnerName + " !";
    }

    public void HideWinnerScreen()
    {
        winnerScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("MainMenu");
    }
}
