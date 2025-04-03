using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FixPlayerCanvas : NetworkBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform.parent; 
    }

    private void LateUpdate()
    {
        float fixedScaleX = (playerTransform.localScale.x < 0) ? -1 : 1;
        transform.localScale = new Vector3(fixedScaleX, 1, 1);
    }
}
