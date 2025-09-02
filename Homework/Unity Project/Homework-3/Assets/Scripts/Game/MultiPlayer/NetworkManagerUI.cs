using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] 
    private Canvas menuCanvas;

    void Start()
    {
        if (menuCanvas == null)
        {
            menuCanvas = GetComponent<Canvas>();
        }
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        HideMenu();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        HideMenu();
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        HideMenu();
    }

    public void ReturnToMainMenu()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("MainMenu");
    }

    private void HideMenu()
    {
        if (menuCanvas != null)
        {
            menuCanvas.enabled = false;
        }
    }
}

