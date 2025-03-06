using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private Vector3 offset;

    [SerializeField]
    private float smoothSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    void FixedUpdate()
    {
        Vector3 desirePosition = player.transform.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothPosition;
    }
}
