using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    [SerializeField]
    private float disappearTime = 2f;
    [SerializeField]
    private float reappearTime = 3f;

    private Rigidbody2D rb;
    private bool hasStartedFalling = false;
    private Vector2 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!hasStartedFalling && rb.velocity.y < -0.1f)
        {
            hasStartedFalling = true;
            StartCoroutine(DisappearAndReappearRoutine());
        }
    }

    IEnumerator DisappearAndReappearRoutine()
    {
        yield return new WaitForSeconds(disappearTime);
        GameObject clone = Instantiate(gameObject, initialPosition, Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(reappearTime);
        clone.GetComponent<DroppingPlatform>().Restart(initialPosition);
    }

    public void Restart(Vector2 position)
    {
        transform.position = position;
        hasStartedFalling = false;
    }
}
