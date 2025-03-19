using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool isJumping = false;
    private bool isOnGround = false;

    [SerializeField]
    private float height = 5;
    [SerializeField]
    private float boxcastDistance = 1.1f;
    [SerializeField]
    private Vector2 boxcastSize = new Vector2(1, 0.5f);
    [SerializeField]
    private LayerMask platformLayerMask;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isOnGround = CheckIfOnGround();
        animator.SetBool("IsJumping", !isOnGround); 

        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, height);
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
