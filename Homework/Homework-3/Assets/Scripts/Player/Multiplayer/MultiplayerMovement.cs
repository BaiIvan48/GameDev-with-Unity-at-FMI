using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class MultiplayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private InputActionReference movementAction;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movementInput;

    private NetworkVariable<float> networkXVelocity = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<float> networkFacingDirection = new NetworkVariable<float>(1f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
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
        if (IsOwner)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if (IsOwner)
        {
            rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);
            networkXVelocity.Value = Mathf.Abs(rb.velocity.x);

            if (Mathf.Abs(rb.velocity.x) > 0.1f)
            {
                networkFacingDirection.Value = Mathf.Sign(movementInput.x);
            }
        }

        animator.SetFloat("xVelocity", networkXVelocity.Value);

        transform.localScale = new Vector3(networkFacingDirection.Value, 1, 1);
    }
}