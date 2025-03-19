using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Movement : MonoBehaviour
{
    private float horizontal = 0;

    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        ResolveLookDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
    }

    void ResolveLookDirection()
    {
        if (Abs(rb.velocity.x) > 0.1f)
        {
            transform.localScale = new Vector3(Sign(rb.velocity.x), 1, 1);
        }
    }
}
