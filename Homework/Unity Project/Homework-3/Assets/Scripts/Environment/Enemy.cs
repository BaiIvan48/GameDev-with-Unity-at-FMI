using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA, pointB;
    public float direction = 1;

    private float speed;
    [SerializeField]
    private float patrolSpeed = 2f;
    [SerializeField]
    private float chargeSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool playerDetected = false;

    [SerializeField] 
    private GameObject radar;
    [SerializeField]
    private float radarDistance = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = patrolSpeed;
    }

    private void FixedUpdate()
    {
        CheckForPlayer();
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void CheckForPlayer()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(radar.transform.position, direction, radarDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
                anim.SetBool("PlayerDetected", true);
                speed = chargeSpeed;
            }
        }
        else
        {
            playerDetected = false;
            anim.SetBool("PlayerDetected", false);
            speed = patrolSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            direction *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            if (playerDetected)
            {
                playerDetected = false;
                speed = patrolSpeed;
                anim.SetBool("PlayerDetected", false);
            }
        }
    }

}
