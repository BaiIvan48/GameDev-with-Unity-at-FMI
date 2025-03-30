using System.Collections;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallWait = 0.0001f;
    [SerializeField]
    private float destroyWait = 2f;
    [SerializeField]
    private float reappearTime = 2f;

    private Rigidbody2D rb;
    private Vector2 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    private IEnumerator FallAndRespawn()
    {
        yield return new WaitForSeconds(fallWait);
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(destroyWait);
        gameObject.SetActive(false);

        //yield return new WaitForSeconds(reappearTime); //won't work because of SetActive(false)
        Invoke("Restart", reappearTime);
    }

    private void Restart()
    {
        gameObject.SetActive(true);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        transform.position = initialPosition;
    }
}
