using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthEffect : MonoBehaviour
{
    [SerializeField]
    private Stats<int> health;

    public Material lowHealthMaterial;

    private int healthValue;
    private int lastHealthValue;

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
        lowHealthMaterial.SetFloat("_Health", healthValue);
    }

    private void Start()
    {
        this.healthValue = health.getValue();
        lowHealthMaterial.SetFloat("_Health", healthValue);
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
