using Unity.Netcode;
using UnityEngine;

public class FixPlayerCanvas : NetworkBehaviour
{
    private void LateUpdate()
    {
        float fixedScaleX = (transform.parent.localScale.x < 0) ? -1 : 1;
        transform.localScale = new Vector3(fixedScaleX, 1, 1);
    }
}
