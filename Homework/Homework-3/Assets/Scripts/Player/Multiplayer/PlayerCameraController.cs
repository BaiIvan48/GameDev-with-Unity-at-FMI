using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    private Camera playerCamera;

    public override void OnNetworkSpawn()
    {
        playerCamera = GetComponentInChildren<Camera>(); 

        if (IsOwner)
        {
            playerCamera.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false); 
        }
    }
}
