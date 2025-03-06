using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private int keysCollected = 0;

    private void OnEnable()
    {
        KeyPickUp.OnKeyPickUp += AddKey;
    }

    private void OnDisable()
    {
        KeyPickUp.OnKeyPickUp -= AddKey;
    }

    private void AddKey()
    {
        keysCollected++;
        Debug.Log("Keys collected: " + keysCollected);
    }
}
