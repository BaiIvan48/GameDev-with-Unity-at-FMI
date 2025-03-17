using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class KeyPickUp : MonoBehaviour
{
    //public static event Action OnKeyPickUp;

    [SerializeField]
    [Range(0, 5)]
    private int collectedCount;
    [SerializeField]
    [Range(0, 5)]
    private int maxCount;

    public Image[] keys;
    public Sprite fullKey;
    public Sprite emptyKey;

    void Start()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].sprite = emptyKey;
            if (i < maxCount)
                keys[i].enabled = true;
            else
                keys[i].enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            collectedCount++;
            UpdateKeys();
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Key"))
    //    {
    //        Destroy(other.gameObject);

    //        if (OnKeyPickUp != null)
    //        {
    //            OnKeyPickUp();
    //        }
    //        collectedCount++;
    //        UpdateKeys();
    //    }
    //}
    void UpdateKeys()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (i < collectedCount)
                keys[i].sprite = fullKey;
            else
                keys[i].sprite = emptyKey;
        }
    }

}
