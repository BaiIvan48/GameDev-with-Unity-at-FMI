using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class MultiplayerJump : NetworkBehaviour
{
    [SerializeField]
    private float jumpForce = 8f;
    [SerializeField]
    private float boxcastDistance = 1.1f;
    [SerializeField]
    private Vector2 boxcastSize = new Vector2(0.8f, 0.5f);
    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private InputActionReference jumpAction;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isOnGround;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        jumpAction.action.Enable();
        jumpAction.action.performed += OnJumpAction;
    }

    private void OnDisable()
    {
        jumpAction.action.performed -= OnJumpAction;
        jumpAction.action.Disable();
    }

    private void FixedUpdate()
    {
        isOnGround = CheckIfOnGround();
        animator.SetBool("IsJumping", !isOnGround);
    }

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        if (IsOwner && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxcastSize, 0f, Vector2.down, boxcastDistance, platformLayerMask);
        return hit.collider != null;
    }
}