using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 ofset = new Vector3(0, 2, 8);

    [SerializeField]
    private float smoothSpeed = 5f;

    void LateUpdate()
    {

        Vector3 desirePosition = target.position + ofset;

        // credits for the smooth part https://www.youtube.com/watch?v=MFQhpwc6cKE
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed*Time.deltaTime);

        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
