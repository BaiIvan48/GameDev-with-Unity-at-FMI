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

    AudioManager audioManager;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            audioManager.PlaySFX(audioManager.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxcastSize, 0f, Vector2.down, boxcastDistance, platformLayerMask);
        if (hit.collider != null)
        {
            float playerBottomY = transform.position.y - (boxcastSize.y / 2f);
            float platformTopY = hit.collider.bounds.max.y;


            if (playerBottomY > platformTopY - 0.05f)
            {
                return true;
            }
        }
        return false;
    }
}