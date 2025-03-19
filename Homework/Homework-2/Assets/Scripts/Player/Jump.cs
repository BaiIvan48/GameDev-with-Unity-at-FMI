using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    bool isJumping = false;
    bool isOnGround = false;

    [SerializeField]
    private float height = 5;
    [SerializeField]
    private float boxcastDistance = 1.1f;
    [SerializeField]
    private Vector2 boxcastSize = new Vector2(1, 0.5f);
    [SerializeField]
    private LayerMask platformLayerMask;

    void Update()
    {
        isOnGround = CheckIfOnGround();
        if (!isJumping)
        {
            isJumping = Input.GetButtonDown("Jump") && isOnGround;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, height), ForceMode2D.Impulse);
            isJumping = false;
        }
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxcastSize, 0f, Vector2.down, boxcastDistance, platformLayerMask);

        if (hit.collider != null)
        {
            float playerBottom = transform.position.y - (boxcastSize.y * 0.5f);
            float platformTop = hit.collider.bounds.max.y;

            return playerBottom >= platformTop - 0.05f;
        }
        return false;
    }

}
