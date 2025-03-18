using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthEffect : MonoBehaviour
{
    [SerializeField]
    Stats<int> health;

    public Material lowHealthMaterial;

    [SerializeField]
    private int healthValue;

    private void OnEnable()
    {
        health.valueUpdateNotify += OnHealthUpdate;
    }

    private void OnDisable()
    {
        health.valueUpdateNotify -= OnHealthUpdate;
    }

    private void OnHealthUpdate(int healthValue)
    {
        this.healthValue = healthValue;
    }

    private void Start()
    {
        this.healthValue = health.getValue();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (healthValue == 1)
        {
            Graphics.Blit(source, destination, lowHealthMaterial);
            return;
        }
        Graphics.Blit(source, destination);
    }
}
