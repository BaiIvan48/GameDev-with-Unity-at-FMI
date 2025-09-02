using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerTransparency : NetworkBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!IsOwner)
        {
            Color color = spriteRenderer.color;
            color.a = 0.55f; 
            spriteRenderer.color = color;
        }
    }
}
