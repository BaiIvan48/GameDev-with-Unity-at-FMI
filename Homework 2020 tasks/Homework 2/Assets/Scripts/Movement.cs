using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    private Rigidbody body;

    private bool isJumping = false;

    [SerializeField]
    private float jumpForce = 10;

    private Vector3 moveDirection;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;

        Vector3 pointToLookAt = transform.position + moveDirection * 100;

        transform.position+=moveDirection;

        transform.LookAt(pointToLookAt);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            body.linearVelocity = new Vector3(body.linearVelocity.x, jumpForce, body.linearVelocity.z);
            isJumping = true;
        }
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

}
