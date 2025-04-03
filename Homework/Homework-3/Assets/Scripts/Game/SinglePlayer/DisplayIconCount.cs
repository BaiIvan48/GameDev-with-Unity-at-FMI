using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIconCount : MonoBehaviour
{
    [SerializeField]
    private int iconCount;

    [SerializeField]
    private GameObject[] images;

    [SerializeField]
    private Stats<int> statOfInterest;

    private void OnEnable()
    {
        statOfInterest.valueUpdateNotify += ActiveIconCount;
    }

    private void OnDisable()
    {
        statOfInterest.valueUpdateNotify -= ActiveIconCount;
    }

    void ActiveIconCount(int n)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i < n);
        }
    }
}
