using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByEnemy : MonoBehaviour
{
    [SerializeField]
    private float knockbackForce = 10f;
    [SerializeField]
    private float hurtDuration = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isHurt = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHurt)
        {
            StartCoroutine(HurtPlayer(collision));
        }
    }

    IEnumerator HurtPlayer(Collision2D collision)
    {
        isHurt = true;
        animator.SetBool("IsHurt", true);

        Vector2 knockbackDirection = (transform.position.x > collision.transform.position.x) ? new Vector2(1, 1) : new Vector2(-1, 1);
        knockbackDirection.Normalize();

        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
        animator.SetBool("IsHurt", false);
    }
}
