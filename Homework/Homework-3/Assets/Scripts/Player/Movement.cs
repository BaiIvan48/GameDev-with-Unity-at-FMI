using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Mathf;

public class Movement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    [SerializeField] 
    private InputActionReference movementAction;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        movementAction.action.Enable();
        movementAction.action.performed += OnMoveInput;
        movementAction.action.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        movementAction.action.performed -= OnMoveInput;
        movementAction.action.canceled -= OnMoveInput;
        movementAction.action.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        ResolveLookDirection();
    }

    private void ResolveLookDirection()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
        }
    }
}
