using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField]
    float direction = -1;

    [SerializeField] float speed = 2;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player"))
            {
                Rigidbody2D playerRb = child.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                   
                    float horizontalInput = Input.GetAxis("Horizontal");  
                    float verticalInput = Input.GetAxis("Vertical");     

                   
                    if (horizontalInput == 0 && verticalInput == 0)
                    {
                        
                        playerRb.MovePosition(playerRb.position + new Vector2(direction * speed * Time.fixedDeltaTime, 0));
                    }
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.SetParent(transform); 
        }
        if (col.CompareTag("Boundary"))
        {
            direction *= -1;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.SetParent(null); 
        }
    }
}
