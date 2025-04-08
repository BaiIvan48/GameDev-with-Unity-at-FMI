using UnityEngine;

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
        SetPlayer(player);
    }
    void FixedUpdate()
    {
        Vector3 desirePosition = player.transform.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothPosition;
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
        offset = transform.position - player.transform.position;
    }
}
