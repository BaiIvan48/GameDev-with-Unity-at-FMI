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
    private void Update()
    {
        if (getValue() <= 0)
        {
            Pickup pickup = FindObjectOfType<Pickup>();
            int keysCollected = (pickup != null) ? pickup.getValue() : 0;
            gameObject.SetActive(false);

            GameManager.Instance.GameOver(keysCollected);
        }
    }
}
