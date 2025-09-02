using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthEffect : MonoBehaviour
{
    [SerializeField]
    private Stats<int> health;

    public Material lowHealthMaterial;

    private int healthValue;
    private bool isSubscribed = false;

    private void Start()
    {
        if (health != null)
        {
            SubscribeToHealth();
        }
        else
        {
            StartCoroutine(WaitForPlayerHealth());
        }
    }

    private IEnumerator WaitForPlayerHealth()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            health = player.GetComponent<Stats<int>>();
            if (health != null)
            {
                SubscribeToHealth();
            }
            else
            {
                Debug.LogWarning("Player found, but no Stats<int> or Health component attached.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found in scene.");
        }
    }

    private void SubscribeToHealth()
    {
        healthValue = health.getValue();
        lowHealthMaterial.SetFloat("_Health", healthValue);
        health.valueUpdateNotify += OnHealthUpdate;
        isSubscribed = true;
    }

    private void OnDisable()
    {
        if (isSubscribed && health != null)
        {
            health.valueUpdateNotify -= OnHealthUpdate;
        }
    }

    private void OnHealthUpdate(int healthValue)
    {
        this.healthValue = healthValue;
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
