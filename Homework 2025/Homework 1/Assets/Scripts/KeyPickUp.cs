using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public static event Action OnKeyPickUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);

            if (OnKeyPickUp != null)
            {
                OnKeyPickUp();
            }
            Debug.Log($"{gameObject.name} picked up a key!");
        }
    }

}
