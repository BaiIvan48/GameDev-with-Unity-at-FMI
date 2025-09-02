using UnityEngine;

public class Pickup : Stats<int>
{
    private void Awake()
    {
        setValue(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            setValue(getValue() + 1);
            Destroy(other.gameObject);
        }
        
    }
}
