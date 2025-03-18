using UnityEngine;

public class Health : Stats<int>
{
    void Awake()
    {
        setValue(5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            setValue(getValue() - 1);
        }
    }
}
