using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 12f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();

        if (rb!=null && rb.CompareTag("Player"))
        {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, bounceForce), ForceMode2D.Impulse);
        }
    }
}
